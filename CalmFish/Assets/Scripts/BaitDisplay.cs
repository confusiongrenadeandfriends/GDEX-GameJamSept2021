using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaitDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    public GameObject BaitIconPrefab = null;
    private GameObject[] BaitIcon = null;

    public void DisplayBait(int number)
    {
        BaitIcon = new GameObject[number];

        for (int i = 0; i < number; i++)
        {
            BaitIcon[i] = Instantiate(BaitIconPrefab, transform.position, Quaternion.identity);
            BaitIcon[i].transform.parent = transform;
            BaitIcon[i].transform.position = transform.position;
            BaitIcon[i].transform.position += new Vector3(0f, 1f * i, 0f);
        }
    }

    public void DecrementBaitDisplay(int index)
    {
        BaitIcon[index].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
