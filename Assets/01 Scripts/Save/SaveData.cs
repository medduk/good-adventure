using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int PlayerMaxHp;
    public int playerCurHp;
    public int playerMaxExp;   // 모아야할 경험치
    public float playerCurExp;   // 현재 경험치
    public float playerLevel;

    public float x;
    public float y;
    public float z;

    public List<int> items = new List<int>();
    public List<int> equip = new List<int>();
}
