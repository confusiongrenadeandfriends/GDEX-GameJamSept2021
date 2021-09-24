using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private Bait _targetedBait = null;

    private float _speed = 0.01f;
    private bool _trackingRandomTarget = false;
    private Vector3 _randomTarget = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        // randomly move

        if (_targetedBait)
        {


            Vector3 distance = transform.position - _targetedBait.transform.position;
            //float distance = Vector3.Distance(transform.position, _targetedBait.transform.position);
            //Vector3 distance = Vector3.Distance (transform.position,  _targetedBait.transform.position);



            distance.z = 0;
            if (distance.magnitude < _speed)
            {
                transform.position = _targetedBait.transform.position;
            }
            else
            {
                transform.position -= distance.normalized * _speed;
            }


        }
        else
        {
            // lazily move around


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
                    transform.position -= distance.normalized * _speed *0.5f;
                }

            }
        }
    }

    public void FollowBait(Bait bait)
    {
        _targetedBait = bait;
    }



}
