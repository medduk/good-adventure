using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class RuneExplanation : MonoBehaviour  // ë£¬ì˜ íš¨ê³¼ë¥¼ ì„¤ëª…í•˜ê¸°ìœ„í•œ ìŠ¤í¬ë¦½íŠ¸
{
    [SerializeField] Runes rune;
    [SerializeField] int level; // runeì˜ ê°¯ìˆ˜
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
<<<<<<< Updated upstream
                    Explanation.text = "ì²´ë ¥ì´ <color=blue>" + level*2 + "</color> ì¦ê°€í•©ë‹ˆë‹¤";
=======
                    Explanation.text = "Ã¼·ÂÀÌ <color=#92F4FF>" + level*2 + "</color> Áõ°¡ÇÕ´Ï´Ù";
>>>>>>> Stashed changes
                    break;
                }
            case Runes.damage:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
<<<<<<< Updated upstream
                    Explanation.text = "ê³µê²©ë ¥ì´ <color=blue>" + level + "</color> ì¦ê°€í•©ë‹ˆë‹¤";
=======
                    Explanation.text = "°ø°İ·ÂÀÌ <color=#92F4FF>" + level + "</color> Áõ°¡ÇÕ´Ï´Ù";
>>>>>>> Stashed changes
                    break;
                }
            case Runes.moveSpeed:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
<<<<<<< Updated upstream
                    Explanation.text = "ì´ë™ì†ë„ê°€ <color=blue>" + level*0.02 + "</color> ì¦ê°€í•©ë‹ˆë‹¤";
=======
                    Explanation.text = "ÀÌµ¿¼Óµµ°¡ <color=#92F4FF>" + level*0.02 + "</color> Áõ°¡ÇÕ´Ï´Ù";
>>>>>>> Stashed changes
                    break;
                }
            case Runes.attackDelay:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
<<<<<<< Updated upstream
                    Explanation.text = "ê³µê²©ì†ë„ê°€ <color=blue>" + level*0.01 + "</color>/s ì¦ê°€í•©ë‹ˆë‹¤";
=======
                    Explanation.text = "°ø°İ¼Óµµ°¡ <color=#92F4FF>" + level*0.01 + "</color>/s Áõ°¡ÇÕ´Ï´Ù";
>>>>>>> Stashed changes
                    break;
                }
            case Runes.defense:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
<<<<<<< Updated upstream
                    Explanation.text = "ë°©ì–´ë ¥ì´ <color=blue>" + level + "</color> ì¦ê°€í•©ë‹ˆë‹¤";
=======
                    Explanation.text = "¹æ¾î·ÂÀÌ <color=#92F4FF>" + level + "</color> Áõ°¡ÇÕ´Ï´Ù";
>>>>>>> Stashed changes
                    break;
                }
            case Runes.criDamage:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
<<<<<<< Updated upstream
                    Explanation.text = "í¬ë¦¬í‹°ì»¬ ë°ë¯¸ì§€ê°€ <color=blue>" + level + "</color>% ì¦ê°€í•©ë‹ˆë‹¤";
=======
                    Explanation.text = "Å©¸®Æ¼ÄÃ µ¥¹ÌÁö°¡ <color=#92F4FF>" + level + "</color>% Áõ°¡ÇÕ´Ï´Ù";
>>>>>>> Stashed changes
                    break;
                }
            case Runes.criProbability:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
<<<<<<< Updated upstream
                    Explanation.text = "í¬ë¦¬í‹°ì»¬ í™•ë¥ ì´ <color=blue>" + level * 0.5 + "</color>% ì¦ê°€í•©ë‹ˆë‹¤";
=======
                    Explanation.text = "Å©¸®Æ¼ÄÃ È®·üÀÌ <color=#92F4FF>" + level * 0.5 + "</color>% Áõ°¡ÇÕ´Ï´Ù";
>>>>>>> Stashed changes
                    break;
                }
            case Runes.aov:
                {
                    level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), rune));
<<<<<<< Updated upstream
                    Explanation.text = "í¡í˜ˆë ¥ì´ <color=blue>" + level *0.5f + "</color>% ì¦ê°€í•©ë‹ˆë‹¤";
=======
                    Explanation.text = "ÈíÇ÷·ÂÀÌ <color=#92F4FF>" + level *0.5f + "</color>% Áõ°¡ÇÕ´Ï´Ù";
>>>>>>> Stashed changes
                    break;
                }
        }
    }
}
