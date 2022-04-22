using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if(instance!=null) return instance;
            
            return null;
        }
    }
    [Header("Now Playing")]
    public AudioSource nowPlaying;

    [Header("Map Sound List")]
    public AudioSource menuBgm;
    public AudioSource tutorialBgm;
    public AudioSource gameBgm;

    /*bossBGM은 나중에 clip을 배열로 만들어서 확장 가능*/
    public AudioSource boss1Bgm;

    [Header("Effect Sound List")]
    public AudioSource buttonsSound;
    public AudioSource itemGetSound;

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

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        
    }

    /* 씬이 로드 될 때 호출. */
    private void OnEnable()
    {
        nowPlaying = menuBgm;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded:" + scene.name);
        Debug.Log(mode);

        if(scene.name == "Loading")
        {
            nowPlaying.Stop();
        }

        if(scene.name == "MainGame")
        {
            nowPlaying.Stop();

            nowPlaying = tutorialBgm;
            nowPlaying.Play();
        }

        if(scene.name == "Menu")
        {
            nowPlaying.Stop();

            nowPlaying = menuBgm;
            nowPlaying.Play();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
