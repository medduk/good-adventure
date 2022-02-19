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

    public GameObject enemyManager;
    private bool enemyInfoLoaded = false;

    private void Awake()
    {
        // Get the Info of 'stage' and 'chapter'
        int chapterIndex = PlayerPrefs.GetInt("ChapterIndex", 0);
        int stageIndex = PlayerPrefs.GetInt("StageIndex", 0);

        int randomMapIndex = UnityEngine.Random.Range(0, chapters[chapterIndex].stagePrefabs[stageIndex].transform.childCount);

        Debug.Log("챕터 번호: " + chapterIndex + " 스테이지 번호: " + stageIndex + " 랜덤맵번호:"+randomMapIndex);
        
        Instantiate(chapters[chapterIndex].stagePrefabs[stageIndex].transform.GetChild(randomMapIndex));
    }

    private void Start()
    {
        StartCoroutine(CheckEnemyLoading());
    }

    IEnumerator CheckEnemyLoading()
    {
        yield return new WaitUntil(() => (enemyManager.transform.childCount != 0));
        //enemyManagerObject = GameObject.Find("EnemyManagerObject");
        enemyInfoLoaded = true;

        Debug.Log("적이 불러 와졌습니다. 적의 수: " + enemyManager.transform.childCount);

        StartCoroutine(StageClear());
    }

    IEnumerator StageClear()
    {
        yield return new WaitUntil(() => enemyManager.transform.childCount == 0);
        Debug.Log("스테이지 클리어!");
        SceneManager.LoadScene("Game");
    }
}
