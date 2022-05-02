using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public enum Runes // 룬의 종류 , 추가 가능
    {
        hp,
        damage,
        attackDelay,
        moveSpeed,
        defense,
        criDamage,
        criProbability,
        aov,
        exp,
        dropPer
    }
public class Rune : MonoBehaviour
{


    [SerializeField] Runes rune;
    [SerializeField] int level; // rune의 갯수
    public TextMeshProUGUI LVText;
    public GameObject Explanation;


    private void Start()
    {
        RunePower();
    }
    public void RunePower() { 
        switch (rune)
        {
            case Runes.hp:
            {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    LVText.text = "LV." + level;
                    break;
            }
            case Runes.damage:
            {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    LVText.text = "LV." + level;
                    break;
            }
            case Runes.attackDelay:
            {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    LVText.text = "LV." + level;
                    break;
            }
            case Runes.moveSpeed:
            {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    LVText.text = "LV." + level;
                    break;
            }
            case Runes.defense:
            {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    LVText.text = "LV." + level;
                    break;
            }
            case Runes.criDamage:
            {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    LVText.text = "LV." + level;
                    break;
            }
            case Runes.criProbability:
            {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    LVText.text = "LV." + level;
                    break;
            }
            case Runes.aov:
            {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
                    LVText.text = "LV." + level;
                    break;
            }
        }
    }




    public void LevelUP()
    {
        RuneUI.instance.Levelup(rune);
        RunePower();
        Explanation.GetComponent<RuneExplanation>().RunePowerWrite();

    }

}
