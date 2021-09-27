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
    [SerializeField]
    private GameObject rockHazardPrefab;
    [SerializeField]
    private GameObject noBaitZonePrefab;

    private void Awake()
    {
        int numberOfReeds = Random.Range(1, 7);
        int numberOfLilyPads = Random.Range(1, 5);
        int numberOfRocks = Random.Range(1, 10);
        int numberOfZones = Random.Range(1, 3);

        Spawn(numberOfReeds, reedsPrefab, false);
        Spawn(numberOfLilyPads, lilypadPrefab, true);
        Spawn(numberOfRocks, rockHazardPrefab, true);
        Spawn(numberOfZones, noBaitZonePrefab, false);

        InputManager.Instance.RandomLevelQuanityUpdate(GameManager.RandomLevelBaitQuanity, GameManager.RandomLevelRockQuanity);
    }

    private void Spawn(int max, GameObject prefab, bool equalScale)
    {
        for (int i = 0; i < max; i++)
        {
            GameObject go = Instantiate(prefab);
            float x = Random.Range(bounds.x, bounds.z);
            float y = Random.Range(bounds.y, bounds.w);
            go.transform.position = new Vector3(x, y, 0);
            float xScale = Random.Range(0.75f, 2);
            float yScale = Random.Range(0.75f, 2);

            if (prefab == rockHazardPrefab)
            {
                xScale /= 2;
                yScale /= 2;
            }

            if (equalScale) go.transform.localScale = new Vector3(xScale, xScale, 1);
            else go.transform.localScale = new Vector3(xScale, yScale, 1);
        }
    }

    internal void ReloadLevel()
    {
        GameManager.RandomLevelBaitQuanity = InputManager.Instance._baitQuantity;
        GameManager.RandomLevelRockQuanity = InputManager.Instance._rockQuantity;
        GameManager.Instance.ReloadLevel();
    }

    //internal IEnumerator WaitThenReloadLevel()
    //{
    //    Debug.Log("Reloading Level");
    //    yield return new WaitForSeconds(2);
    //    GameManager.RandomLevelBaitQuanity = InputManager.Instance._baitQuantity;
    //    GameManager.RandomLevelRockQuanity = InputManager.Instance._rockQuantity;
    //    GameManager.Instance.ReloadLevel();
    //}
}
