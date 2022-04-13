using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Runes
    {
        hp,
        damage,
        moveSpeed,
        attackDelay,
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
    [SerializeField] int level; // runeÀÇ °¹¼ö
    public TextMeshProUGUI LVText;
    public TextMeshProUGUI CountText;

    private void Start()
    {
        runepower();
    }
    public void runepower() { 
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
            case Runes.moveSpeed:
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
}
