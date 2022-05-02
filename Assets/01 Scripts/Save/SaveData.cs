using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public int PlayerMaxHp; // ����� �ִ�ü��
    public int playerCurHp; // ����� ����ü��

    public int Coin; // ����� �������

    public int playerMaxExp;   // ����� ��ƾ��� ����ġ
    public float playerCurExp;   // ����� ���� ����ġ
    public float playerLevel; // ����� ����

    /* ����� �÷��̾��� ��ġ */
    public float x; 
    public float y;
    public float z;

    public int[] Levelablilty = new int[System.Enum.GetNames(typeof(Ablilty)).Length]; // ����� �ɷ�ġ ����������

    /* �������� ��� ID�ڵ带 �̿��Ͽ� �����ϰ� �ҷ��ö� �������� �ٷ� ��� �����ϴ� ����� ��� */
    public List<int> items = new List<int>(); // ����� ������ ��������
    public List<int> equip = new List<int>(); // ����� ������ �������
}
