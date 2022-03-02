using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public Button btn;
    public Sprite[] img;

    public GameObject pauseImage;
    public GameObject gameOverImage;
    public DialogManager dialogManager;

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
            btn.GetComponent<Image>().sprite = img[0];
            Time.timeScale = 0f;
            
        }
        else
        {
            Time.timeScale = 1f;
            btn.GetComponent<Image>().sprite = img[1];
        }
    }

    public void RestartMap()
    {
        StageManager.Instance.RestartGame();
        pauseImage.SetActive(false);
    }

    public void InitGame()
    {
        dialogManager.tutoNumber = 0;
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