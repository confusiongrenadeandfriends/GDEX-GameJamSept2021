using System.Collections;
using System.IO;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject winText;

    public void winLevel()
    {
        winText.SetActive(true);
        int baitScore = InputManager.Instance._baitQuantity * 100;
        int rockScore = InputManager.Instance._rockQuantity * 50;
        GameManager.Instance.Score += baitScore + rockScore + 500;
        StartCoroutine(waitThenLoadNextLevel());
    }

    private IEnumerator waitThenLoadNextLevel()
    {
        yield return new WaitForSeconds(4);

        GameManager.Instance.LoadNextLevel();
    }
}