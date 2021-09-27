using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static GameManager Instance;
    private static int _score;
    public static int Score
    {
        get => _score;
        set
        {
            _score = value;
            GameObject.FindGameObjectWithTag("Score").GetComponent<Text>().text = "Score: " + value.ToString();
        }
    }

    internal static int RandomLevelBaitQuanity = 10;
    internal static int RandomLevelRockQuanity = 5;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>().onClick.AddListener(StartGame);
            GameObject.FindGameObjectWithTag("QuitButton").GetComponent<Button>().onClick.AddListener(QuitGame);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public static void StartGame()
    {
        Score = 0;
        LoadNextLevel();
    }

    public static void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void TitleScreen()
    {
        RandomLevelBaitQuanity = 10;
        RandomLevelRockQuanity = 5;
        SceneManager.LoadScene(0);
    }

    public static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
