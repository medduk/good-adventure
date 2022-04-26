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

    IEnumerator LoadScene()
    {
        yield return null;
        operation = SceneManager.LoadSceneAsync("MainGame");
        operation.allowSceneActivation = false;
        while (!operation.isDone)
        {
            yield return null;

            if (progressbar.value < 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 0.9f, Time.deltaTime);
            }

            else if(operation.progress >= 0.9f)
            {
                progressbar.value = Mathf.MoveTowards(progressbar.value, 1f, Time.deltaTime);
            }

            if (progressbar.value >= 1f && operation.progress >= 0.9f )
            {
                if (ContinueDataManager.isContinuousGame) // 이어하던 기록이 있다면
                {
                    loadtext.text = "Loading Complete";
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
