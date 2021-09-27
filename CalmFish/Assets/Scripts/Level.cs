using System.Collections;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject winText;
    private GameManager gameManager;
    private bool levelDone;
    public GameObject tutorialText;
    private RandomLevel randomLevel;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game").GetComponent<GameManager>();
        levelDone = false;
        if (!TryGetComponent(out randomLevel)) StartCoroutine(waitThenDisableTutorial());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void winLevel()
    {
        if (!levelDone)
        {
            if (randomLevel) randomLevel.ReloadLevel();
            else
            {
                winText.SetActive(true);
                levelDone = true;
                StartCoroutine(waitThenLoadNextLevel());
            }
        }
    }

    private IEnumerator waitThenDisableTutorial()
    {
        yield return new WaitForSeconds(4);

        tutorialText.SetActive(false);
    }

    private IEnumerator waitThenLoadNextLevel()
    {
        yield return new WaitForSeconds(4);

        GameManager.LoadNextLevel();
    }
}