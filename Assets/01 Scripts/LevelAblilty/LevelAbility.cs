using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum Ablilty
{
    hpUp,
    damageUp,
    moveSpeedUp,
    defenseUp,
    criDamageUp,
    criProbabilityUp,
    aovUp,
    gold,

}
public class LevelAbility : MonoBehaviour
{
    bool Canchoose;
    [SerializeField] GameObject LevelUPUI;
    [SerializeField] Button[] btns;
    [SerializeField] Image[] images;
    [SerializeField] TextMeshProUGUI[] Texts;
    [SerializeField] Sprite[] sprites;

    List<int[]> LVAblilty = new List<int[]>();
    List<int[]> randomAbilityTable = new List<int[]>();
    List<int[]> OptionsPlayerCanChoose = new List<int[]>();

    public Transform buttonScale;
    Vector3 defaultScale;
    // ÀÎµ¦½º,È®·ü,ÃÖ´ë°­È­Ä¡ 
    int[] HpUp = { 0, 7, 5 };
    int[] DamageUp = { 1, 7, 4 };
    int[] SpeedUp = { 2, 7, 3 };
    int[] DefenseUp = { 3, 7, 4 };
    int[] CriDamageUp = { 4, 7, 3 };
    int[] CriProbabliltyUp = { 5, 7, 3 };
    int[] AovUp = { 6, 7, 3 };
    int[] Gold = { 7, 7, 10 };

    int sum;
    // Start is called before the first frame update
    void Start()
    {
        defaultScale = buttonScale.localScale;
        LVAblilty.Add(HpUp);
        LVAblilty.Add(DamageUp);
        LVAblilty.Add(SpeedUp);
        LVAblilty.Add(DefenseUp);
        LVAblilty.Add(CriDamageUp);
        LVAblilty.Add(CriProbabliltyUp);
        LVAblilty.Add(AovUp);
        LVAblilty.Add(Gold);
        ReadyPlayerLevelUP();
    }

    public void ReadyPlayerLevelUP()
    {
        
        for(int i=0; i < PlayerStatus.Instance.Levelablilty.Length; i++)
        {
            if(PlayerStatus.Instance.Levelablilty[i] < LVAblilty[i][2])
            {
                randomAbilityTable.Add(LVAblilty[i]);
            }
        }

        for(int i=0; i<3; i++)
        {
            ChooseRandomAbilityOption();
            RewriteAbilityOptionWindow(i);
        }
    }

    public void ChooseRandomAbilityOption()
    {
        sum = 0;

        for (int i = 0; i < randomAbilityTable.Count; i++)
        {
            sum += randomAbilityTable[i][1];
        }

        if (sum != 0)
        {
            int randomIndex = Random.Range(1, sum + 1);

            int j = 0;
            while (j < randomAbilityTable.Count)
            {
                randomIndex = randomIndex - randomAbilityTable[j][1];
                if (randomIndex <= 0)
                {
                    break;
                }
                j++;
            }
            OptionsPlayerCanChoose.Add(randomAbilityTable[j]);
            randomAbilityTable.RemoveAt(j);
        }

        if(sum == 0)
        {
            OptionsPlayerCanChoose.Add(LVAblilty[(int)Ablilty.gold]);
        }
    }

    void RewriteAbilityOptionWindow(int i)
    {
        switch (OptionsPlayerCanChoose[i][0])
        {
            case (int)Ablilty.hpUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "Ã¼·Â\n<color=#92F4FF>20</color>»ó½Â\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.damageUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "°ø°Ý·Â\n<color=#92F4FF>15</color>»ó½Â\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.moveSpeedUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "ÀÌµ¿¼Óµµ\n<color=#92F4FF>0.5</color>»ó½Â\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.defenseUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "¹æ¾î·Â\n<color=#92F4FF>5</color>»ó½Â\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.criDamageUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "Ä¡¸íÅ¸µ¥¹ÌÁö\n<color=#92F4FF>10</color>%»ó½Â\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.criProbabilityUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "Ä¡¸íÅ¸È®·ü\n<color=#92F4FF>5</color>%»ó½Â\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.aovUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "ÈíÇ÷·Â\n<color=#92F4FF>5</color>%»ó½Â\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.gold:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "°ñµå\n<color=#92F4FF>¾Æ¹«Æ°</color>%È¹µæ\n";
                break;
        }
    }
    public void OpenUI()
    {
        for (int j = 0; j < btns.Length; j++)
        {
            btns[j].gameObject.SetActive(true);
        }
        Canchoose = true;
        LevelUPUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Select(int i)
    {
        if (Canchoose)
        {
            switch (OptionsPlayerCanChoose[i][0])
            {
                case (int)Ablilty.hpUp:
                    PlayerStatus.Instance.PlayerMaxHp += 20;
                    PlayerStatus.Instance.RecoveryHp(20);
                    break;
                case (int)Ablilty.damageUp:
                    PlayerStatus.Instance.PlayerDamage += 15;
                    break;
                case (int)Ablilty.moveSpeedUp:
                    PlayerStatus.Instance.PlayerMoveSpeed += (float)0.5f;
                    break;
                case (int)Ablilty.defenseUp:
                    PlayerStatus.Instance.PlayerDefense += 5;
                    break;
                case (int)Ablilty.criDamageUp:
                    PlayerStatus.Instance.CriticalDamage += 10;
                    break;
                case (int)Ablilty.criProbabilityUp:
                    PlayerStatus.Instance.CriticalProbability += 5f;
                    break;
                case (int)Ablilty.aovUp:
                    PlayerStatus.Instance.AbsorptionOfVitality += (float)0.05;
                    break;
                case (int)Ablilty.gold:
                    Debug.Log("°ñµåÈ¹µæ");
                    break;
            }
            Canchoose = false;
            SoundManager.Instance.chooseAbilitySound.Play();
        }

        if (OptionsPlayerCanChoose[i][0] < LVAblilty.Count - 1)
        {
            PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] += 1;
        }

        StartCoroutine(ShowSelcetedAbilityOptionWindow(i));
    }
    IEnumerator ShowSelcetedAbilityOptionWindow(int i)
    {
        for(int j = 0; j < btns.Length; j++)
        {
            btns[j].gameObject.SetActive(false);
        }
        btns[i].gameObject.SetActive(true);
        btns[i].transform.localScale = defaultScale * 1.2f;
        yield return new WaitForSecondsRealtime(1f);
        LevelUPUI.SetActive(false);
        btns[i].transform.localScale = defaultScale;
        Time.timeScale = 1f;

        randomAbilityTable.Clear();
        OptionsPlayerCanChoose.Clear();
        ReadyPlayerLevelUP();
    }
}
