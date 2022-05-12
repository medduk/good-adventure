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
    [Header("Data")]
    public Transform dataManaer;

    [Header("Maps")]
    [SerializeField] Chapter[] chapters;
    [SerializeField] GameObject[] hiddenMapPrefabs;
    public int randomMaxRange = 10;
    [SerializeField] bool wasHidden = false;

    private GameObject player;
    private GameObject portal;

    public GameObject enemyManager;
    public Transform mapManager;

    public bool isClear = false;

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

        if (!ContinueDataManager.isContinuousGame)  // 이어하기가 없다면 초기화.
        {
            MoveToLobby();
        }
        else
        {
            PlayerStatus.Instance.LoadGame();
            SetMap(PlayerPrefs.GetInt("MapIndex",-1));  // 있다면 초기화 하지않고 저장되어 있는 맵 인덱스를 불러옴.
        }

    }
    private void SetMap(int _mapIndex = -1)
    {
        player.transform.position = new Vector2(100, 100);
        int mapIndex = _mapIndex;

        // Get the Info of 'stage' and 'chapter'
        chapterIndex = PlayerPrefs.GetInt("ChapterIndex", 0);
        stageIndex = PlayerPrefs.GetInt("StageIndex", 0);

        if (chapterIndex == 0)  // 만약 로비라면 이어하기 상태가 저장되지 않음.
        {
            isClear = true;
            ContinueDataManager.SetContinueGame(false);
        }
        else
        {
            isClear = false;
            ContinueDataManager.SetContinueGame(true);
        }

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

        if (SoundManager.Instance != null)
        {
            ChangeStageSound(mapVal.name.ToLower());    //소리변경
        }
        StartCoroutine(CheckEnemyLoading());
    }

    public void MoveToLobby()
    {
        InitIndex();
        SetMap();
    }

    IEnumerator CheckEnemyLoading()
    {
        yield return new WaitUntil(() => mapVal.gameObject.activeSelf == true);
        player.transform.position = mapVal.transform.Find("StartPoint").position;
        portal = mapVal.transform.Find("Portal").gameObject;
        portal.SetActive(false);

        StartCoroutine(StageClear());
    }

    IEnumerator StageClear()
    {
        yield return new WaitUntil(() => enemyManager.transform.childCount == 0);
        Debug.Log("스테이지 클리어!");
        if(portal != null) portal.SetActive(true);

        isClear = true;
    }

    public void MoveNextStage()
    {
        if (isClear)
        {
            mapVal.gameObject.SetActive(false);

            int mapItemCount = dataManaer.childCount;

            Debug.Log("아이템 갯수: " + mapItemCount +" 아이템 없어집니다.");
            if (mapItemCount > 0)
            {
                for(int i=0; i< mapItemCount; i++)
                {
                    Destroy(dataManaer.GetChild(i).gameObject);
                }
            }

            /* Random Hidden Map Access */
            int hidden = (int)UnityEngine.Random.Range(0, randomMaxRange);
            if (hidden == 0 && !wasHidden && stageIndex != 0)   // 튜토리얼 일 때는 발생 안함
            {
                wasHidden = true;
                SetHiddenMap();
                
                return;
            }

            wasHidden = false;

            maxStage = chapters[chapterIndex].stagePrefabs.Length;

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
                    maxStage = chapters[chapterIndex].stagePrefabs.Length;
                }
                else
                {
                    Debug.Log("개발 중");
                    InitIndex();
                }
            }
            PlayerStatus.Instance.SaveGame();   // 맵이 넘어가면서 저장됨.
            SetMap();
        }
    }
    void ChangeStageSound(string stageName)
    {
        AudioSource tempPlaying = SoundManager.Instance.nowPlaying;

        if(chapterIndex == 0)
        {
            SoundManager.Instance.nowPlaying = SoundManager.Instance.tutorialBgm;
        }
        else if (stageName.Contains("boss"))
        {
            SoundManager.Instance.nowPlaying = SoundManager.Instance.boss1Bgm;
        }
        else if(stageName.Contains("stage") && chapterIndex == 1)
        {
            SoundManager.Instance.nowPlaying = SoundManager.Instance.gameBgm;
        }
        

        if (SoundManager.Instance.nowPlaying == tempPlaying)    // 만약 바꾸려는 음악과 현재 재생되는 음악이 같다면 굳이 안바꿔도됨.
        {
            return;
        }

        tempPlaying.Stop();

        SoundManager.Instance.nowPlaying.Play();
    }

    void SetHiddenMap(int hiddenMapIndex = 0)
    {
        mapVal = Instantiate(hiddenMapPrefabs[hiddenMapIndex]).transform;
        mapVal.SetParent(mapManager);
        mapVal.gameObject.SetActive(true);
        player.transform.position = mapVal.transform.Find("StartPoint").position;

        StartCoroutine(CheckEnemyLoading());
    }

    public void RestartGame(bool isDev = false) // 게임 재시작
    {
        foreach (Transform et in enemyManager.GetComponentInChildren<Transform>())  // 적들을 다 없애줌.
        {
            if (et.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                Destroy(et.gameObject);
            }
        }

        int mapItemCount = dataManaer.childCount;

        if (mapItemCount > 0)             // 아이템 삭제
        {
            for (int i = 0; i < mapItemCount; i++)
            {
                Destroy(dataManaer.GetChild(i).gameObject);
            }
        }

        Destroy(mapVal.gameObject); // 맵을 지운다.

        if (isDev)
        {
            /*완전 초기화 이므로 전부 삭제*/
            foreach (Transform mt in mapManager.GetComponentInChildren<Transform>())    // Map Manager 오브젝트에 있는 맵들 다 삭제(메모리관리)
            {
                if (mt.name.Contains("stage"))
                {
                    Destroy(mt.gameObject);
                }
            }
            MoveToLobby();
            return;
        }

        if(wasHidden)
        {
            SetHiddenMap();
            return;
        }
        SetMap(PlayerPrefs.GetInt("MapIndex"));
    }
}