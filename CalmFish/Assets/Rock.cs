using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public GameObject Ripple;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = Instantiate(Ripple, transform);
        go.transform.position = transform.position;
        Destroy(gameObject, 3);
    }

    private void Update()
    {
        gameObject.transform.localScale -= new Vector3(0.3f * Time.deltaTime, 0.3f * Time.deltaTime, 0);
    }
}
