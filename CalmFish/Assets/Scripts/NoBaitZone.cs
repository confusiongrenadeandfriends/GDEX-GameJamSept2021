using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoBaitZone : MonoBehaviour
{

    private SpriteRenderer renderer;
    private RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        rectTransform = gameObject.GetComponent<RectTransform>();

        renderer.size = rectTransform.rect.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
