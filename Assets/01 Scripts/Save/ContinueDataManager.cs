using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContinueDataManager : MonoBehaviour
{
    public static bool isContinuousGame = PlayerPrefs.GetInt("ContinueGame", 0) == 1 ? true : false;
    // 만약 이어하기가 있다면 1, 없다면 0.
    public static bool skip = PlayerPrefs.GetInt("Skip") == 1 ? true : false;
    // 만약 튜토리얼 스킵을 했다면 1 , 없다면 0

    public static void SetContinueGame(bool _isContinuousGame) // 이어하기 여부 확인
    {
        isContinuousGame = _isContinuousGame;
        PlayerPrefs.SetInt("ContinueGame", _isContinuousGame ? 1 : 0);
    }

    public static void SetSkip() // 전 게임으로부터 스킵을 했는지 안했는지 유지하기 위함 
    {
        if (skip)
        {
            skip = false;
            PlayerPrefs.SetInt("Skip", 0);
            
        }
        else
        {
            skip = true;
            PlayerPrefs.SetInt("Skip", 1);
        }
    }


                    
                    
                
                   
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
