using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseImage;
    public GameObject gameOverImage;

    public bool isPaused = false;

    private bool isGameOver = false;

    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance != null) return instance;
            return null;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        pauseImage.SetActive(false);
        gameOverImage.SetActive(false);
    }

    private void SwitchPause(bool check)
    {
        isPaused = check;
        pauseImage.SetActive(check);
    }

    public void Puase()
    {
        SwitchPause(!isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void RestartMap()
    {
        StageManager.Instance.RestartGame();
        pauseImage.SetActive(false);
    }

    public void InitGame()
    {
        StageManager.Instance.RestartGame(true);
        pauseImage.SetActive(false);
        if (isGameOver)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void SetGameOver()
    {
        gameOverImage.SetActive(true);
        isGameOver = true;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}