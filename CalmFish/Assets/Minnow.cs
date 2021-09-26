using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minnow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }


    private Bait _targetedBait = null;

    protected float _speed = 0.02f;
    private bool _trackingRandomTarget = false;
    private Vector3 _randomTarget = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        // randomly move

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
        if (distance.magnitude < _speed)
        {
            transform.position = _targetedBait.transform.position;
            _targetedBait.gameObject.SetActive(false);

            // win level

            // disable throwing objects

            // display victory messgae

            // Load next level
        }
        else
        {
            transform.position -= distance.normalized * _speed;
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        Vector3 direction = (transform.position - collision.gameObject.transform.position).normalized;
        transform.position -= -direction;
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
            if (distance.magnitude < _speed)
            {
                transform.position = _randomTarget;
                _trackingRandomTarget = false;
            }
            else
            {
                transform.position -= distance.normalized * _speed * 0.5f;
            }

        }
    }
}
