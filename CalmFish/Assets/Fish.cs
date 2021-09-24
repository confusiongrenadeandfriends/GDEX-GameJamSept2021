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
    }

    public void FollowBait(Bait bait)
    {
        _targetedBait = bait;
    }



}
