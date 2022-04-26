using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueDataManager : MonoBehaviour
{
    public static bool isContinuousGame = PlayerPrefs.GetInt("ContinueGame", 0) == 1 ? true : false;
    // ���� �̾��ϱⰡ �ִٸ� 1, ���ٸ� 0.

    public static void SetContinueGame(bool _isContinuousGame)
    {
        isContinuousGame = _isContinuousGame;
        PlayerPrefs.SetInt("ContinueGame", _isContinuousGame ? 1 : 0);
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
