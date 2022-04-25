using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueDataManager : MonoBehaviour
{
    public static bool isContinuousGame = PlayerPrefs.GetInt("ContinueGame", 0) == 1 ? true : false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
