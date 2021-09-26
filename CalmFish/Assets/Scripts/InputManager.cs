using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameObject RockPrefab = null;
    public GameObject BaitPrefab = null;
    public Camera MainCamera = null;
    public GameObject FishPrefab = null;
    public GameObject MinnowPrefab = null;
    private Fish _fish = null;
    private Minnow _minnow = null;
    private int _baitQuantity = 3;
    private int _rockQuantity = 3;
    // Start is called before the first frame update
    private void Start()
    {

        _fish = Instantiate(FishPrefab, transform).GetComponent<Fish>();
        _minnow = Instantiate(MinnowPrefab, transform).GetComponent<Minnow>();



    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_baitQuantity > 0)
            {
                Bait bait = SpawnBait();
                _fish.FollowBait(bait);
                _minnow.FollowBait(bait);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (_rockQuantity > 0)
            {
                SpawnRock();
            }
        }
    }

    private Bait SpawnBait()
    { 
        _baitQuantity--;
        GameObject bait = Instantiate(BaitPrefab, transform);
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = MainCamera.ScreenToWorldPoint(mousePos);
        worldPos.z = 1f;
        bait.transform.position = worldPos;
        return bait.GetComponent<Bait>();
    }

    private void SpawnRock()
    {
        _rockQuantity--;
        GameObject rock = Instantiate(RockPrefab, transform);
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = MainCamera.ScreenToWorldPoint(mousePos);
        worldPos.z = 1f;
        rock.transform.position = worldPos;
    }
}