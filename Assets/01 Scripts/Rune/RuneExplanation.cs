using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RuneExplanation : MonoBehaviour
{


    [SerializeField] Runes rune;
    [SerializeField] int level; // rune�� ����
    public TextMeshProUGUI Explanation;

    private void Start()
    {
        RunePowerWrite();
    }
    public void RunePowerWrite()
    {
        switch (rune)
        {
            case Runes.hp:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "ü���� <color=blue>" + level*2 + "</color> �����մϴ�";
                    break;
                }
            case Runes.damage:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "���ݷ��� <color=blue>" + level + "</color> �����մϴ�";
                    break;
                }
            case Runes.moveSpeed:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "�̵��ӵ��� <color=blue>" + level*0.02 + "</color> �����մϴ�";
                    break;
                }
            case Runes.attackDelay:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "���ݼӵ��� <color=blue>" + level*0.01 + "</color>/s �����մϴ�";
                    break;
                }
            case Runes.defense:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "������ <color=blue>" + level + "</color> �����մϴ�";
                    break;
                }
            case Runes.criDamage:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "ũ��Ƽ�� �������� <color=blue>" + level + "</color>% �����մϴ�";
                    break;
                }
            case Runes.criProbability:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "ũ��Ƽ�� Ȯ���� <color=blue>" + level * 0.5 + "</color>% �����մϴ�";
                    break;
                }
            case Runes.aov:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "�������� <color=blue>" + level *0.5f + "</color>% �����մϴ�";
                    break;
                }
        }
    }
}
