using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{

    public GameObject Ripple = null;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(Ripple, transform.parent);
        go.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
