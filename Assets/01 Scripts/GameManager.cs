using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject puaseImage;

    public bool isPaused = false;

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
        puaseImage.SetActive(false);
    }

    private void SwitchPause(bool check)
    {
        isPaused = check;
        puaseImage.SetActive(check);
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
    }

    public void InitGame()
    {
        StageManager.Instance.RestartGame(true);
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