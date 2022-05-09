using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RuneExplanation : MonoBehaviour  // 룬의 효과를 설명하기위한 스크립트
{
    [SerializeField] Runes rune;
    [SerializeField] int level; // rune의 갯수
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
                    Explanation.text = "체력이 <color=blue>" + level*2 + "</color> 증가합니다";
                    break;
                }
            case Runes.damage:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "공격력이 <color=blue>" + level + "</color> 증가합니다";
                    break;
                }
            case Runes.moveSpeed:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "이동속도가 <color=blue>" + level*0.02 + "</color> 증가합니다";
                    break;
                }
            case Runes.attackDelay:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "공격속도가 <color=blue>" + level*0.01 + "</color>/s 증가합니다";
                    break;
                }
            case Runes.defense:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "방어력이 <color=blue>" + level + "</color> 증가합니다";
                    break;
                }
            case Runes.criDamage:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "크리티컬 데미지가 <color=blue>" + level + "</color>% 증가합니다";
                    break;
                }
            case Runes.criProbability:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "크리티컬 확률이 <color=blue>" + level * 0.5 + "</color>% 증가합니다";
                    break;
                }
            case Runes.aov:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    Explanation.text = "흡혈력이 <color=blue>" + level *0.5f + "</color>% 증가합니다";
                    break;
                }
        }
    }
}
