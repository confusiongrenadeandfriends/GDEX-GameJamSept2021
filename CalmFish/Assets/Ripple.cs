using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector3 GrowthSpeed = new Vector3(0.1f, 0.1f, 0.1f);


    // Update is called once per frame
    void Update()
    {

        transform.localScale += GrowthSpeed;

        if (transform.localScale.x >= 20)
        {
            Destroy(this.gameObject);
        }
        
    }
}
