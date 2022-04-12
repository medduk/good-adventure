using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : MonoBehaviour
{
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

    [SerializeField] Runes rune;
    [SerializeField] int level;
    public void runepower() { 
        switch (rune)
        {
            case Runes.hp:
            { 
                break;
            }
            case Runes.damage:
            { 
                break;
            }
            case Runes.moveSpeed:
            {
                break;
            }
            case Runes.attackDelay:
            {
                break;
            }
            case Runes.defense:
            {
                break;
            }
            case Runes.criDamage:
            {
                break;
            }
            case Runes.criProbability:
            {
                break;
            }
            case Runes.aov:
            {
                break;
            }
        }
    }


}
