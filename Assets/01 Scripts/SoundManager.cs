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

    [Header("Menu & UI Effect Sound List")]
    public AudioSource buttonsSound;
    public AudioSource LevelUpSound;
    public AudioSource chooseAbilitySound;
    public AudioSource GameOverSound;
    public AudioSource buySound;

    [Header("InGame Object Effect Sound ")]
    public AudioSource itemGetSound;
    public AudioSource enemyHitSound;
    public AudioSource playerHitSound;
    public AudioSource arrowShootSound;

    public AudioSource[] bgms;
    public AudioSource[] effects;
    public Transform bgm;
    public Transform effect;

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
        bgms = bgm.GetComponentsInChildren<AudioSource>();
        effects = effect.GetComponentsInChildren<AudioSource>();
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
        //Debug.Log(mode);

        if(scene.name == "Loading")
        {
            nowPlaying.Stop();
        }

        //if(scene.name == "MainGame")
        //{
        //    nowPlaying.Stop();

        //    nowPlaying = tutorialBgm;
        //    nowPlaying.Play();
        //}

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
    public void SoundON()
    {
        for(int i = 0; i < bgms.Length; i++)
        {
            bgms[i].mute = false;
        }
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].mute = false;
        }
    }
    public void SoundOFF()
    {
        for (int i = 0; i < bgms.Length; i++)
        {
            bgms[i].mute = true;
        }
        for (int i = 0; i < effects.Length; i++)
        {
            effects[i].mute = true;
        }
    }
}
