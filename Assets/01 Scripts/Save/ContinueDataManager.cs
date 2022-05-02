using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ContinueDataManager : MonoBehaviour
{

    public static bool isContinuousGame = PlayerPrefs.GetInt("ContinueGame", 0) == 1 ? true : false;
    // ���� �̾��ϱⰡ �ִٸ� 1, ���ٸ� 0.
    public static bool skip = PlayerPrefs.GetInt("Skip") == 1 ? true : false;
    // ���� Ʃ�丮�� ��ŵ�� �ߴٸ� 1 , ���ٸ� 0

    public static void SetContinueGame(bool _isContinuousGame) // �̾��ϱ� ���� Ȯ��
    {
        isContinuousGame = _isContinuousGame;
        PlayerPrefs.SetInt("ContinueGame", _isContinuousGame ? 1 : 0);
    }

    public static void Setskip() // �� �������κ��� ��ŵ�� �ߴ��� ���ߴ��� �����ϱ� ���� 
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
