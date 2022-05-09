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
    ricochetShot,
    multiShot,
    chainShot,
}
public class LevelAbility : MonoBehaviour
{
    bool Canchoose;
    [SerializeField] GameObject LevelUPUI;
    [SerializeField] Button[] btns;
    [SerializeField] Image[] images;
    [SerializeField] TextMeshProUGUI[] Texts;
    [SerializeField] Sprite[] sprites;

    List<int[]> LVAblilty = new List<int[]>();  // 레벨업능력의 총 리스트
    List<int[]> randomAbilityTable = new List<int[]>();  // 업그레이드가 가능한 레벨업 능력의 총 리스트
    List<int[]> OptionsPlayerCanChoose = new List<int[]>(); // randomAbilityTable 에서 랜덤으로 정해준 개수 만큼 뽑아올 리스트

    public Transform buttonScale;
    Vector3 defaultScale;
    // 인덱스,확률,최대강화치 
    int[] HpUp = { 0, 7, 5 };
    int[] DamageUp = { 1, 7, 4 };
    int[] SpeedUp = { 2, 7, 3 };
    int[] DefenseUp = { 3, 7, 4 };
    int[] CriDamageUp = { 4, 7, 3 };
    int[] CriProbabliltyUp = { 5, 7, 3 };
    int[] AovUp = { 6, 7, 3 };
    int[] Gold = { 7, 7, -1 };
    int[] RichchetShot = { 8, 7, 1 };
    int[] MultiShot = { 9, 7, 1 };
    int[] ChainetShot = { 10, 7, 1 };

    int sum;
    void Start()  
    {
        /* 레벨업능력 초기화 */
        defaultScale = buttonScale.localScale;
        LVAblilty.Add(HpUp);
        LVAblilty.Add(DamageUp);
        LVAblilty.Add(SpeedUp);
        LVAblilty.Add(DefenseUp);
        LVAblilty.Add(CriDamageUp);
        LVAblilty.Add(CriProbabliltyUp);
        LVAblilty.Add(AovUp);
        LVAblilty.Add(Gold);
        LVAblilty.Add(RichchetShot);
        LVAblilty.Add(MultiShot);
        LVAblilty.Add(ChainetShot);
        ReadyPlayerLevelUP();
    }

    public void ReadyPlayerLevelUP()  // 레벨업 목록 랜덤 뽑기
    {
        
        for(int i=0; i < System.Enum.GetNames(typeof(Ablilty)).Length; i++)
        {
            if(PlayerStatus.Instance.Levelablilty[i] < LVAblilty[i][2])  // 레벨업능력이 최대레벨이 아닐경우
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

    public void ChooseRandomAbilityOption()  // 레벨업 가능 능력 리스트에서 확률에따라 정해준 개수만큼 뽑는 함수
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

        if(sum == 0)  // 더이상 남은 레벨업 능력이 남아있지 않다면 무한루프용 골드지급을 제공
        {
            OptionsPlayerCanChoose.Add(LVAblilty[(int)Ablilty.gold]);
        }
    }

    void RewriteAbilityOptionWindow(int i) // 레벨업능력 UI 그리기
    {
        switch (OptionsPlayerCanChoose[i][0])
        {
            case (int)Ablilty.hpUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "체력\n<color=#92F4FF>20</color>상승\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.damageUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "공격력\n<color=#92F4FF>15</color>상승\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.moveSpeedUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "이동속도\n<color=#92F4FF>0.5</color>상승\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.defenseUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "방어력\n<color=#92F4FF>5</color>상승\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.criDamageUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "치명타데미지\n<color=#92F4FF>10</color>%상승\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.criProbabilityUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "치명타확률\n<color=#92F4FF>5</color>%상승\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.aovUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "흡혈력\n<color=#92F4FF>5</color>%상승\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.gold:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "골드\n<color=#92F4FF>"+ (PlayerStatus.Instance.PlayerLevel+1)*50 +"</color>획득";
                break;
            case (int)Ablilty.ricochetShot:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "바운스샷\n<color=#92F4FF>기본공격이 튕깁니다.</color>";
                break;
            case (int)Ablilty.multiShot:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "멀티샷\n<color=#92F4FF>기본공격이 두개로 늘어납니다.</color>";
                break;
            case (int)Ablilty.chainShot:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "체인샷\n<color=#92F4FF>기본공격을 두번씩 발동합니다.</color>";
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
    public void Select(int i)  // 레벨업 결정시 효과
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
                    inventory.instance.GetOrGiveCoin((int)PlayerStatus.Instance.PlayerLevel * 50);
                    break;
                case (int)Ablilty.ricochetShot:
                    PlayerStatus.Instance.playerSkills[0] = 1;
                    break;
                case (int)Ablilty.multiShot:
                    PlayerStatus.Instance.playerSkills[1] = 1;
                    break;
                case (int)Ablilty.chainShot:
                    PlayerStatus.Instance.playerSkills[2] = 1;
                    break;
            }
            Canchoose = false;
            SoundManager.Instance.chooseAbilitySound.Play();


            if (OptionsPlayerCanChoose[i][0] != (int)Ablilty.gold) // 골드는 무한루프이기 때문에 골드지급은 레벨업을 하지않음
            {
                PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] += 1;
            }

            StartCoroutine(ShowSelcetedAbilityOptionWindow(i));
        }
    }
    IEnumerator ShowSelcetedAbilityOptionWindow(int i)  // 시각적 효과
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
