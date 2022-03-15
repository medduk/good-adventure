using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject s;
    public Button pausebtn,statusbtn;
    public Sprite[] img;

    public GameObject pauseImage,statusImage;
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
        //statusImage.SetActive(true);
        gameOverImage.SetActive(false);

        isPaused = false;
    }

    private void SwitchPause(bool check)
    {
        isPaused = check;
    }

    public void Puase()
    {
        if (!statusImage.gameObject.activeSelf)
        {
            SwitchPause(!isPaused);
            if (isPaused)
            {
                pausebtn.GetComponent<Image>().sprite = img[0];
                Time.timeScale = 0f;
                pauseImage.SetActive(isPaused);
            }
            else
            {
                Time.timeScale = 1f;
                pausebtn.GetComponent<Image>().sprite = img[1];
                pauseImage.SetActive(isPaused);
            }
        }
    }
    public void Status()
    {
        if (!pauseImage.gameObject.activeSelf)
        {
            SwitchPause(!isPaused);
            if (isPaused)
            {
                Time.timeScale = 0f;
                statusImage.SetActive(isPaused);
            }
            else
            {
                Time.timeScale = 1f;
                statusImage.SetActive(isPaused);
                s.GetComponent<ShowStatus>().NowChange();
            }
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