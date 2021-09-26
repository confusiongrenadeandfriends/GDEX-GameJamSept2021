using System.Collections;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject winText;
    public GameObject tutorialText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitThenDisableTutorial());
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

    private IEnumerator waitThenDisableTutorial()
    {
        yield return new WaitForSeconds(4);

        tutorialText.SetActive(false);
    }

    private IEnumerator waitThenLoadNextLevel()
    {
        yield return new WaitForSeconds(4);

        GameManager.Instance.LoadNextLevel();
    }
}