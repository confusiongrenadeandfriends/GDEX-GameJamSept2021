using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minnow : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

        inputManager = GameObject.Find("Game").GetComponent<InputManager>();

        inputManager.baitEvent.AddListener(followBait);
    }


    void followBait()
    {
        FollowBait(inputManager.CurrentBait);
    }

    private Bait _targetedBait = null;

    private InputManager inputManager = null;
    [SerializeField]
    protected float _speed = 7.0f;
    private bool _trackingRandomTarget = false;
    private Vector3 _randomTarget = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        // randomly move

        if (InLillyPad)
        { _targetedBait = null; }

        if (_targetedBait)
        {
            ChaseAfterBait();
        }
        else
        {
            // lazily move around
            MoveRandomly();
        }
    }

    public void FollowBait(Bait bait)
    {
        _targetedBait = bait;
    }


    // move directly after the bait when it is avaiable
    private void ChaseAfterBait()
    {
        Vector3 distance = transform.position - _targetedBait.transform.position;

        distance.z = 0;
        if (distance.magnitude < (_speed * Time.deltaTime))
        {
            transform.position = _targetedBait.transform.position;
            _targetedBait.EatBait();
            _targetedBait.gameObject.SetActive(false);
            _targetedBait = null;

            // win level

            // disable throwing objects

            // display victory messgae

            // Load next level
        }
        else
        {
            transform.position -= distance.normalized * _speed * Time.deltaTime;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        Ripple ripple = collision.GetComponent<Ripple>();
        if (ripple && ripple.rippleParent == Ripple.RippleParent.Rock)
        {
            Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
            transform.position -= -direction;
        }
    }

    private bool InLillyPad = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Reeds temp = collision.GetComponent<Reeds>();
        if (ReferenceEquals(temp, null) == false)
        {
            _speed /= 10f;
        }

        LillyPad lilly = collision.GetComponent<LillyPad>();
        if (ReferenceEquals(lilly, null) == false)
        {
            _targetedBait = null;
            InLillyPad = true;
        }

        RockHazard rock = collision.GetComponent<RockHazard>();
        if (ReferenceEquals(rock, null) == false)
        {
            Vector3 direction;
            if (rock.IsBorder)
            {
                direction = (transform.position - transform.parent.position).normalized;
                Vector3 target = direction + Random.insideUnitSphere;
                target.z = 0;
                _randomTarget = transform.parent.position; ////Random.insideUnitSphere + Random.insideUnitSphere; // target;
                _trackingRandomTarget = true;

            }
            else
            {
                direction = (transform.position - collision.gameObject.transform.position).normalized * _speed * 10f * Time.deltaTime;

                transform.position -= -direction;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        Reeds temp = collision.GetComponent<Reeds>();
        if (ReferenceEquals(temp, null) == false)
        {
            _speed *= 2f;
        }

        LillyPad lilly = collision.GetComponent<LillyPad>();
        if (ReferenceEquals(lilly, null) == false)
        {
            _targetedBait = null;
            InLillyPad = false;
        }
    }


    // randomly move when there is no bait to chase
    private void MoveRandomly()
    {
        if (_trackingRandomTarget == false)
        {
            Vector3 target = transform.position + Random.insideUnitSphere;
            target.z = 0;
            _randomTarget = target;
            _trackingRandomTarget = true;
        }
        else
        {
            Vector3 distance = transform.position - _randomTarget;
            distance.z = 0;
            if (distance.magnitude < (_speed * Time.deltaTime))
            {
                transform.position = _randomTarget;
                _trackingRandomTarget = false;
            }
            else
            {
                transform.position -= distance.normalized * _speed * 0.5f * Time.deltaTime;
            }

        }
    }
}
