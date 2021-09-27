using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        level = GameObject.Find("Level").GetComponent<Level>();
        _inputManager = GameObject.Find("Game").GetComponent<InputManager>();
        _inputManager.baitEvent.AddListener(followBait);
    }


    void followBait()
    {
        FollowBait(_inputManager.CurrentBait);
    }

    private InputManager _inputManager = null;
    public bool _targetFish;
    private Bait _targetedBait = null;
    private Level level;

    [SerializeField]
    protected float _speed = 3.0f;
    private bool _trackingRandomTarget = false;
    private Vector3 _randomTarget = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        // randomly move

        if (_targetedBait  && _targetedBait.IsEaten == false)
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

            if (_targetFish)
            {
                InputManager.Instance.Won = true;
                int baitScore = InputManager.Instance._baitQuantity * 10;
                int rockScore = InputManager.Instance._rockQuantity * 20;
                GameManager.Score += baitScore + rockScore + 100;
                level.winLevel();
            }

            _targetedBait.gameObject.SetActive(false);
            // disable throwing objects

        }
        else
        {
            transform.position -= distance.normalized * _speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Reeds temp = collision.GetComponent<Reeds>();
        if (ReferenceEquals(temp, null) == false)
        {
            _speed /= 2f;
        }

        LillyPad lilly = collision.GetComponent<LillyPad>();
        if (ReferenceEquals(lilly, null) == false)
        {
            _targetedBait = null;
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
