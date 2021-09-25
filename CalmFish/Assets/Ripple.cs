using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ripple : MonoBehaviour
{
    private float colliderGrowth = 1;
    private float _maxScale = 10;
    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        if (transform.parent.name.ToLower().Contains("bait")) _maxScale = 10;
        else if (transform.parent.name.ToLower().Contains("rock")) _maxScale = 20;
    }

    public Vector3 GrowthSpeed = new Vector3(0.1f, 0.1f, 0.1f);


    // Update is called once per frame
    void Update()
    {
        circleCollider.radius += Time.deltaTime * colliderGrowth;
        transform.localScale += GrowthSpeed;

        if (transform.localScale.x >= _maxScale)
        {
            Destroy(this.gameObject);
        }
        
    }
}
