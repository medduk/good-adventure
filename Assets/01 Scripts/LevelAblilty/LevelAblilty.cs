using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public enum Ablilty
{
    hpup,
    damageup,
    moveSpeedup,
    defenseup,
    criDamageup,
    criProbabilityup,
    aovup,
    gold,

}
public class LevelAblilty : MonoBehaviour
{
    [SerializeField] GameObject LevelUPUI;
    [SerializeField] Button[] btns;
    [SerializeField] Image[] images;
    [SerializeField] TextMeshProUGUI[] Texts;
    [SerializeField] Sprite[] sprites;

    List<int[]> LVAblilty = new List<int[]>();
    List<int[]> randomset = new List<int[]>();
    List<int[]> choose = new List<int[]>();

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
        LevelUPUI.SetActive(false);
        LVAblilty.Add(HpUp);
        LVAblilty.Add(DamageUp);
        LVAblilty.Add(SpeedUp);
        LVAblilty.Add(DefenseUp);
        LVAblilty.Add(CriDamageUp);
        LVAblilty.Add(CriProbabliltyUp);
        LVAblilty.Add(AovUp);
        LVAblilty.Add(Gold);
        playerLevelUPReady();
    }
    public void playerLevelUPReady()
    {
        
        for(int i=0; i < PlayerStatus.Instance.Levelablilty.Length; i++)
        {
            if(PlayerStatus.Instance.Levelablilty[i] < LVAblilty[i][2])
            {
                randomset.Add(LVAblilty[i]);
            }
        }

        for(int i=0; i<3; i++)
        {
            randomchoose();
            changeshow(i);
        }
    }

    public void randomchoose()
    {
        sum = 0;
        for (int i = 0; i < randomset.Count; i++)
        {
            sum += randomset[i][1];
        }
        if (sum != 0)
        {
            int randomIndex = Random.Range(1, sum + 1);

            int j = 0;
            while (j < randomset.Count)
            {
                randomIndex = randomIndex - randomset[j][1];
                if (randomIndex <= 0)
                {
                    break;
                }
                j++;
            }
            choose.Add(randomset[j]);
            randomset.RemoveAt(j);
        }

        if(sum == 0)
        {
            choose.Add(LVAblilty[7]);
            
        }
    }

    void changeshow(int i)
    {
        switch (choose[i][0])
        {
            case (int)Ablilty.hpup:
                images[i].sprite = sprites[choose[i][0]];
                Texts[i].text = "Ã¼·Â\n<color=#92F4FF>20</color>»ó½Â\n" + PlayerStatus.Instance.Levelablilty[choose[i][0]] + "/" + choose[i][2];
                break;
            case (int)Ablilty.damageup:
                images[i].sprite = sprites[choose[i][0]];
                Texts[i].text = "°ø°Ý·Â\n<color=#92F4FF>20</color>»ó½Â\n" + PlayerStatus.Instance.Levelablilty[choose[i][0]] + "/" + choose[i][2];
                break;
            case (int)Ablilty.moveSpeedup:
                images[i].sprite = sprites[choose[i][0]];
                Texts[i].text = "ÀÌµ¿¼Óµµ\n<color=#92F4FF>0.5</color>»ó½Â\n" + PlayerStatus.Instance.Levelablilty[choose[i][0]] + "/" + choose[i][2];
                break;
            case (int)Ablilty.defenseup:
                images[i].sprite = sprites[choose[i][0]];
                Texts[i].text = "¹æ¾î·Â\n<color=#92F4FF>5</color>»ó½Â\n" + PlayerStatus.Instance.Levelablilty[choose[i][0]] + "/" + choose[i][2];
                break;
            case (int)Ablilty.criDamageup:
                images[i].sprite = sprites[choose[i][0]];
                Texts[i].text = "Ä¡¸íÅ¸µ¥¹ÌÁö\n<color=#92F4FF>10</color>%»ó½Â\n" + PlayerStatus.Instance.Levelablilty[choose[i][0]] + "/" + choose[i][2];
                break;
            case (int)Ablilty.criProbabilityup:
                images[i].sprite = sprites[choose[i][0]];
                Texts[i].text = "Ä¡¸íÅ¸È®·ü\n<color=#92F4FF>5</color>%»ó½Â\n" + PlayerStatus.Instance.Levelablilty[choose[i][0]] + "/" + choose[i][2];
                break;
            case (int)Ablilty.aovup:
                images[i].sprite = sprites[choose[i][0]];
                Texts[i].text = "ÈíÇ÷·Â\n<color=#92F4FF>5</color>%»ó½Â\n" + PlayerStatus.Instance.Levelablilty[choose[i][0]] + "/" + choose[i][2];
                break;
            case (int)Ablilty.gold:
                images[i].sprite = sprites[choose[i][0]];
                Texts[i].text = "°ñµå\n<color=#92F4FF>¾Æ¹«Æ°</color>%È¹µæ\n";
                break;
        }
    }
    public void openUI()
    {
        LevelUPUI.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Select(int i)
    {
        switch(choose[i][0])
        {
            case (int)Ablilty.hpup:
                PlayerStatus.Instance.PlayerMaxHp += 20;
                PlayerStatus.Instance.RecoveryHp(20);
                break;
            case (int)Ablilty.damageup:
                PlayerStatus.Instance.PlayerDamage += 15;
                break;
            case (int)Ablilty.moveSpeedup:
                PlayerStatus.Instance.PlayerMoveSpeed += (float)0.5f;
                break;
            case (int)Ablilty.defenseup:
                PlayerStatus.Instance.PlayerDefense += 5;
                break;
            case (int)Ablilty.criDamageup:
                PlayerStatus.Instance.CriticalDamage += 10;
                break;
            case (int)Ablilty.criProbabilityup:
                PlayerStatus.Instance.CriticalProbability += 5f;
                break;
            case (int)Ablilty.aovup:
                PlayerStatus.Instance.AbsorptionOfVitality += (float)0.05;
                break;
            case (int)Ablilty.gold:
                Debug.Log("°ñµåÈ¹µæ");
                break;


        }
        if (choose[i][0] < LVAblilty.Count - 1)
        {
            PlayerStatus.Instance.Levelablilty[choose[i][0]] += 1;
        }

        StartCoroutine(selcetshow(i));
    }
    IEnumerator selcetshow(int i)
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

        randomset.Clear();
        choose.Clear();
        playerLevelUPReady();
    }
}
