using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bait : MonoBehaviour
{

    public GameObject Ripple = null;

    public bool IsEaten { get; private set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(Ripple, transform);
        go.transform.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EatBait()
    {
        IsEaten = true;
    }
}
