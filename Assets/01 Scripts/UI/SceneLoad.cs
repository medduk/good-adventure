using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoad : MonoBehaviour
{
    public GameObject imageContinueWindow;

    public Slider progressbar;
    public Text loadtext;

    public AsyncOperation operation;

    private void Awake()
    {
        if (imageContinueWindow != null)
        {
            imageContinueWindow.SetActive(true);    // For Initialize, It is closed next step.
            imageContinueWindow.SetActive(false);   // Close the window.
        }
    }

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene() // 로딩하는 시각적 효과를 내기 위하여 
    {
        yield return null;
        operation = SceneManager.LoadSceneAsync("MainGame");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            yield return null;

            if (progressbar.value < 0.9f) // 시각적으로 답답함을 덜어주기 위한 
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }

            else if(operation.progress >= 0.9f) // 90%에서 로딩바를 멈추고 효과실제로 로딩이 끝났으면 100%로 다시 채워준다
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if (progressbar.value >= 1f && operation.progress >= 0.9f )
            {
                if (ContinueDataManager.isContinuousGame) // 이어하던 기록이 있다면
                {
                    loadtext.color = new Color(1, 1, 1, 0);
                    progressbar.transform.position = new Vector3(progressbar.transform.position.x, progressbar.transform.position.y - 1000);
                    if (!imageContinueWindow.activeSelf) imageContinueWindow.SetActive(true);
                }
                else
                {
                    loadtext.text = "Touch Anywhere";
                    if(Input.GetMouseButtonDown(0))
                    {
                        operation.allowSceneActivation = true;
                    }
                }   
            }
            //if (Input.GetMouseButtonDown(0) && progressbar.value >= 1f && operation.progress >= 0.9f)
            //{
            //    operation.allowSceneActivation = true;
            //}
        }
    }
    public void ChooseContinueGame()
    {
        operation.allowSceneActivation = true;
    }

    public void ChooseGoLobby()
    {
        InitIndex();
        ContinueDataManager.SetContinueGame(false);
        operation.allowSceneActivation = true;
    }

    private void InitIndex(int index = 0)
    {
        PlayerPrefs.SetInt("ChapterIndex", index);
        PlayerPrefs.SetInt("StageIndex", index);
    }
}
