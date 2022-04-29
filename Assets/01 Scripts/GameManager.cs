using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public GameObject realstatus,bossHPbar,Runestart,SHOPstart;
    public Button pausebtn,statusbtn,Exitbtn,removebtn;
    public Sprite[] img;
    public Image bossIcon;

    public GameObject pauseImage,statusImage,ReinforceImage,ItemInformationImage;
    public GameObject gameOverImage;
    public DialogManager dialogManager;

    public bool isPaused = false;

    private bool isGameOver = false;    // Check GameOver
    private int BossCount = 0;          // Boss stage can have many boss

    private static GameManager instance = null;

    public GameObject Who;
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
        statusImage.SetActive(true);
        gameOverImage.SetActive(false);
        ReinforceImage.SetActive(true);
        ItemInformationImage.SetActive(true);
        bossHPbar.SetActive(true);
        SHOPstart.SetActive(true);
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
                realstatus.GetComponent<ShowStatus>().NowChange();
                statusImage.SetActive(isPaused);
            }
            else
            {
                Time.timeScale = 1f;
                statusImage.SetActive(isPaused);
 
            }
        }
    }
    public void OpenReinForce()
    {
        pausebtn.gameObject.SetActive(false);
        statusbtn.gameObject.SetActive(false);
        Exitbtn.gameObject.SetActive(true);
        ReinforceImage.SetActive(true);
        ReinForce.instance.ReinForceStart();
    }
    public void closeReinForce()
    {
        Exitbtn.gameObject.SetActive(false);
        pausebtn.gameObject.SetActive(true);
        statusbtn.gameObject.SetActive(true);
        ReinForce.instance.ReinForceEnd();
        ReinforceImage.SetActive(false);
        DialogON();
    }

    public void OpenItemInformation(bool can)
    {
        ItemInformationImage.gameObject.SetActive(true);
        removebtn.gameObject.SetActive(can);
    }
    public void OpenBossHpbar()
    {
        BossCount++;
        bossHPbar.gameObject.SetActive(true);      
    }
    public void CloseBossHpbar()
    {
        BossCount--;
        if (BossCount == 0)
        {
            bossHPbar.gameObject.SetActive(false);
            /* boss클리어 시 발동 사운드 등*/
            SoundManager.Instance.nowPlaying.Stop();

            SoundManager.Instance.nowPlaying = SoundManager.Instance.tutorialBgm;
            SoundManager.Instance.nowPlaying.Play();
        }

    }

    public void changeBossIcon(Sprite Icon)
    {
        bossIcon.sprite = Icon;
    }

    public void OpenRune()
    {
        Runestart.gameObject.SetActive(true);
    }
    public void CloseRune()
    {
        Runestart.gameObject.SetActive(false);
        DialogON();
    }
    public void OpenSHOP()
    {
        SHOPstart.gameObject.SetActive(true);
    }
    public void CloseSHOP()
    {
        SHOPstart.gameObject.SetActive(false);
        DialogON();
    }

    void DialogON()
    {
        if (Who != null)
        {
            dialogManager.Action(Who , 1);
            Who = null;
        }
    }
    public void RestartMap()
    {
        StageManager.Instance.RestartGame();
        pauseImage.SetActive(false);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }

    public void InitGame()  // 죽었을 때 사용.
    {
        dialogManager.tutoNumber = 0;
        StageManager.Instance.RestartGame(true);
        pauseImage.SetActive(false);
        gameOverImage.SetActive(false);
    }

    public void SetGameOver()
    {
        ContinueDataManager.SetContinueGame(false);

        gameOverImage.SetActive(true);
        isGameOver = true;
    }
    
    public void SetGameLiving()
    {
        isGameOver = false;
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