using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLevel : MonoBehaviour
{
    public Vector4 bounds;
    [SerializeField]
    private GameObject reedsPrefab;
    [SerializeField]
    private GameObject lilypadPrefab;

    private void Awake()
    {
        int numberOfReeds = Random.Range(1, 5);
        int numberOfLilyPads = Random.Range(1, 5);

        Spawn(numberOfReeds, reedsPrefab, false);
        Spawn(numberOfLilyPads, lilypadPrefab, true);
    }

    private void Spawn(int max, GameObject prefab, bool equalScale)
    {
        for (int i = 0; i < max; i++)
        {
            GameObject go = Instantiate(prefab);
            float x = Random.Range(bounds.x, bounds.z);
            float y = Random.Range(bounds.y, bounds.w);
            go.transform.position = new Vector3(x, y, 0);
            float xScale = Random.Range(1, 3);
            float yScale = Random.Range(1, 3);

            if (equalScale) go.transform.localScale = new Vector3(xScale, xScale, 1);
            else go.transform.localScale = new Vector3(xScale, yScale, 1);
        }
    }
}
