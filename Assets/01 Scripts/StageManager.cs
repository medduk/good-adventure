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
    private GameObject portal;

    public GameObject enemyManager;
    public Transform mapManager;

    private bool isClear = false;

    private int maxChapter;
    private int maxStage;

    private int chapterIndex;
    private int stageIndex;
    private Transform mapVal;
    private int randomMapIndex;

    private static StageManager instance = null;
    public static StageManager Instance
    {
        get
        {
            if (instance != null) return instance;

            return null;
        }
    }

    private void InitIndex(int index = 0)
    {
        PlayerPrefs.SetInt("ChapterIndex", index);
        PlayerPrefs.SetInt("StageIndex", index);
    }

    private void Awake()
    {
        InitIndex();

        if (instance == null)
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
    private void SetMap(int _mapIndex = -1)
    {
        player.transform.position = new Vector2(100, 100);
        isClear = false;
        int mapIndex = _mapIndex;

        // Get the Info of 'stage' and 'chapter'
        chapterIndex = PlayerPrefs.GetInt("ChapterIndex", 0);
        stageIndex = PlayerPrefs.GetInt("StageIndex", 0);

        if (_mapIndex < 0)
        {
            randomMapIndex = UnityEngine.Random.Range(0, chapters[chapterIndex].stagePrefabs[stageIndex].transform.childCount);
            mapIndex = randomMapIndex;
        }
        PlayerPrefs.SetInt("MapIndex", mapIndex);

        Debug.Log("챕터 번호: " + chapterIndex + " 스테이지 번호: " + stageIndex + " 랜덤맵번호:" + randomMapIndex);

        mapVal = Instantiate(chapters[chapterIndex].stagePrefabs[stageIndex].transform.GetChild(mapIndex));
        mapVal.SetParent(mapManager);
        mapVal.gameObject.SetActive(true);

        StartCoroutine(CheckEnemyLoading());
    }

    IEnumerator CheckEnemyLoading()
    {
        yield return new WaitUntil(() => (enemyManager.transform.childCount != 0));
        player.transform.position = mapVal.transform.Find("StartPoint").position;
        portal = mapVal.transform.Find("Portal").gameObject;
        portal.SetActive(false);

        StartCoroutine(StageClear());
    }

    IEnumerator StageClear()
    {
        yield return new WaitUntil(() => enemyManager.transform.childCount == 0);
        Debug.Log("스테이지 클리어!");
        portal.SetActive(true);

        isClear = true;
    }

    public void MoveNextStage()
    {
        if (isClear)
        {
            mapVal.gameObject.SetActive(false);
            if (++stageIndex < maxStage)
            {
                PlayerPrefs.SetInt("StageIndex", stageIndex);
            }
            else
            {
                if (++chapterIndex < maxChapter)
                {
                    PlayerPrefs.SetInt("ChapterIndex", chapterIndex);
                    PlayerPrefs.SetInt("StageIndex", 0);
                }
                else
                {
                    Debug.Log("개발 중");
                    InitIndex();
                }
            }
            SetMap();
        }
    }

    public void RestartGame(bool isDev = false)
    {
        foreach (Transform et in enemyManager.GetComponentInChildren<Transform>())
        {
            if (et.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Destroy(et.gameObject);
            }
        }

        Destroy(mapVal.gameObject);
        if (isDev)
        {
            /*완전 초기화 이므로 전부 삭제*/
            foreach (Transform mt in mapManager.GetComponentInChildren<Transform>())
            {
                if (mt.name.Contains("stage"))
                {
                    Destroy(mt.gameObject);
                }
            }
            InitIndex();
            SetMap();
            return;
        }
        SetMap(PlayerPrefs.GetInt("MapIndex"));
    }
}