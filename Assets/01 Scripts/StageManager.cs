using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

[Serializable]
class Chapter
{
    public GameObject[] stagePrefabs;
}

public class StageManager : MonoBehaviour
{
    [SerializeField] Chapter[] chapters;
    private GameObject player;

    public GameObject enemyManager;

    private bool isClear = false;

    private int maxChapter;
    private int maxStage;

    private int chapterIndex;
    private int stageIndex;
    private Transform mapVal;

    private static StageManager instance = null;
    public static StageManager Instance
    {
        get
        {
            if (instance != null) return instance;

            return null;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        player = GameObject.FindWithTag("Player");

        maxChapter = chapters.Length;
        maxStage = chapters[chapterIndex].stagePrefabs.Length;


        SetMap();
    }

    private void SetMap()
    {
        isClear = false;
        player.transform.position = Vector3.zero;

        // Get the Info of 'stage' and 'chapter'
        chapterIndex = PlayerPrefs.GetInt("ChapterIndex", 0);
        stageIndex = PlayerPrefs.GetInt("StageIndex", 0);

        int randomMapIndex = UnityEngine.Random.Range(0, chapters[chapterIndex].stagePrefabs[stageIndex].transform.childCount);

        Debug.Log("챕터 번호: " + chapterIndex + " 스테이지 번호: " + stageIndex + " 랜덤맵번호:" + randomMapIndex);

        mapVal = Instantiate(chapters[chapterIndex].stagePrefabs[stageIndex].transform.GetChild(randomMapIndex));
        mapVal.gameObject.SetActive(true);

        StartCoroutine(CheckEnemyLoading());
    }

    IEnumerator CheckEnemyLoading()
    {
        yield return new WaitUntil(() => (enemyManager.transform.childCount != 0));

        StartCoroutine(StageClear());
    }

    IEnumerator StageClear()
    {
        yield return new WaitUntil(() => enemyManager.transform.childCount == 0);
        Debug.Log("스테이지 클리어!");

        isClear = true;
    }

    public void MoveNextStage()
    {
        if (isClear)
        {
            mapVal.gameObject.SetActive(false);
            if(stageIndex < maxStage)
            {
                PlayerPrefs.SetInt("StageIndex", ++stageIndex);
            }
            else
            {
                if(chapterIndex<maxChapter)
                {
                    PlayerPrefs.SetInt("ChapterIndex", ++chapterIndex);
                    PlayerPrefs.SetInt("StageIndex", 0);
                }
                else
                {
                    Debug.Log("개발 중");
                }
            }
            SetMap();
        }
    }
}
