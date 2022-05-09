using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int PlayerMaxHp; // 저장시 최대체력
    public int playerCurHp; // 저장시 현재체력

    public int Coin; // 저장시 보유골드

    public int playerMaxExp;   // 저장시 모아야할 경험치
    public float playerCurExp;   // 저장시 현재 경험치
    public float playerLevel; // 저장시 레벨

    /* 저장시 플레이어의 위치 */
    public float x; 
    public float y;
    public float z;

    public int[] Levelablilty = new int[System.Enum.GetNames(typeof(Ablilty)).Length]; // 저장시 능력치 레벨업상태

    /* 아이템의 경우 ID코드를 이용하여 저장하고 불러올때 아이템을 바로 즉시 지급하는 방식을 사용 */
    public List<int> items = new List<int>(); // 저장시 아이템 보유상태
    public List<int> equip = new List<int>(); // 저장시 아이템 착용상태
}
