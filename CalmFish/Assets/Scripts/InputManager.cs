using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    internal static InputManager Instance;
    public UnityEvent baitEvent;
    
    public GameObject RockPrefab = null;
    public GameObject BaitPrefab = null;
    public Camera MainCamera = null;
    public GameObject FishPrefab = null;
    public GameObject MinnowPrefab = null;
    private Fish _fish = null;
    //private Minnow[] _minnow = null;
    public int _baitQuantity = 3;
    public int _rockQuantity = 3;
    public Bait CurrentBait { get; private set; } = null;
    internal bool Won = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {


        if (baitEvent == null)
            baitEvent = new UnityEvent();

        //_fish = Instantiate(FishPrefab, transform).GetComponent<Fish>();
        //_minnow = Instantiate(MinnowPrefab, transform).GetComponent<Minnow>();

        //baitEvent = new Event();
        // baitEvent.Invoke

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_baitQuantity > 0)
            {
                Bait bait = SpawnBait();
                // _fish.FollowBait(bait);
                if (CurrentBait) Destroy(CurrentBait.gameObject);
                CurrentBait = bait;
                baitEvent.Invoke();
               // _minnow.FollowBait(bait);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (_rockQuantity > 0)
            {
                SpawnRock();
            }
        }

        if (CurrentBait != null && CurrentBait.gameObject.activeSelf == false)
        {
            Debug.Log($"Bait: {_baitQuantity}");
            if (!Won && _baitQuantity <= 0)
            {
                GameManager.Instance.ReloadLevel();
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