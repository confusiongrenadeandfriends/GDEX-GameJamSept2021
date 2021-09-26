using System.Collections;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject winText;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void winLevel()
    {
        winText.SetActive(true);
        StartCoroutine(waitThenLoadNextLevel());
    }

    private IEnumerator waitThenLoadNextLevel()
    {
        yield return new WaitForSeconds(4);

        gameManager.LoadNextLevel();
    }
}