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
    public GameObject MinnowPrefab = null;
    private Fish _fish = null;
    //private Minnow[] _minnow = null;
    public int _baitQuantity = 3;
    public int _rockQuantity = 3;
	public List<GameObject> noBaitZones;
    public BaitDisplay baitDisplay = null;
    public BaitDisplay rockDisplay = null;
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

        if (FindObjectOfType<RandomLevel>() == false)
        {
            baitDisplay.DisplayBait(_baitQuantity);
            rockDisplay.DisplayBait(_rockQuantity);
        }
    }

    internal void RandomLevelQuanityUpdate(int baitQuanity, int rockQuanity)
    {
        _baitQuantity = baitQuanity;
        _rockQuantity = rockQuanity;

        baitDisplay.DisplayBait(_baitQuantity);
        rockDisplay.DisplayBait(_rockQuantity);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (_baitQuantity > 0 && noBaitZones.TrueForAll(obj => !RectTransformUtility.RectangleContainsScreenPoint(obj.GetComponent<RectTransform>(), Input.mousePosition, Camera.main)))
            {
                Bait bait = SpawnBait();
                // _fish.FollowBait(bait);
                if (CurrentBait) Destroy(CurrentBait.gameObject);
                CurrentBait = bait;
                baitEvent.Invoke();
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if (_rockQuantity > 0)
            {
                SpawnRock();
            }
        }

        //if (FindObjectOfType<RandomLevel>())
        //{
        //    bool isNull = (CurrentBait == null);
        //    bool isActive = false;
        //    if (!isNull) isActive = CurrentBait.gameObject.activeSelf;
        //    Debug.Log($"{isNull}, {isActive}, {_baitQuantity}");
        //}

        bool baitEaten = CurrentBait != null && CurrentBait.gameObject.activeSelf == false;
        bool baitLost = CurrentBait == null && _baitQuantity <= 0;
        if (baitEaten || baitLost)
        {
            if (FindObjectOfType<RandomLevel>() && _baitQuantity <= 0)
            {
                GameManager.Instance.TitleScreen();
            } 
            else if (!Won && _baitQuantity <= 0)
            {
                GameManager.Instance.ReloadLevel();
            }

        }
    }

    private Bait SpawnBait()
    { 
        _baitQuantity--;

        baitDisplay.DecrementBaitDisplay(_baitQuantity);
        GameObject bait = Instantiate(BaitPrefab, transform);
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 1f;
        bait.transform.position = worldPos;
        return bait.GetComponent<Bait>();
    }

    private void SpawnRock()
    {
        _rockQuantity--;
        rockDisplay.DecrementBaitDisplay(_rockQuantity);
        GameObject rock = Instantiate(RockPrefab, transform);
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        worldPos.z = 1f;
        rock.transform.position = worldPos;
    }
}