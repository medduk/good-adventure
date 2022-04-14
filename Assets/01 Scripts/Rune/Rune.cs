using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

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
public class Rune : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{


    [SerializeField] Runes rune;
    [SerializeField] int level; // runeÀÇ °¹¼ö
    public TextMeshProUGUI LVText;
    public GameObject Explanation;

    bool click;
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
    IEnumerator stoplevelup()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (click)
        {
            click = false;

        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {

            if (click)
            {
                StopCoroutine("stoplevelup");
                click = false;
                RuneUI.instance.Levelup(rune);
                RunePower();
                Explanation.GetComponent<RuneExplanation>().RunePowerWrite();

        }
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {

            click = true;
            StartCoroutine("stoplevelup");
        
    }
}
