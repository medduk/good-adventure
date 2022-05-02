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

    List<int[]> LVAblilty = new List<int[]>();  // �������ɷ��� �� ����Ʈ
    List<int[]> randomAbilityTable = new List<int[]>();  // ���׷��̵尡 ������ ������ �ɷ��� �� ����Ʈ
    List<int[]> OptionsPlayerCanChoose = new List<int[]>(); // randomAbilityTable ���� �������� ������ ���� ��ŭ �̾ƿ� ����Ʈ

    public Transform buttonScale;
    Vector3 defaultScale;
    // �ε���,Ȯ��,�ִ밭ȭġ 
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
        /* �������ɷ� �ʱ�ȭ */
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

    public void ReadyPlayerLevelUP()  // ������ ��� ���� �̱�
    {
        
        for(int i=0; i < System.Enum.GetNames(typeof(Ablilty)).Length; i++)
        {
            if(PlayerStatus.Instance.Levelablilty[i] < LVAblilty[i][2])  // �������ɷ��� �ִ뷹���� �ƴҰ��
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

    public void ChooseRandomAbilityOption()  // ������ ���� �ɷ� ����Ʈ���� Ȯ�������� ������ ������ŭ �̴� �Լ�
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

        if(sum == 0)  // ���̻� ���� ������ �ɷ��� �������� �ʴٸ� ���ѷ����� ��������� ����
        {
            OptionsPlayerCanChoose.Add(LVAblilty[(int)Ablilty.gold]);
        }
    }

    void RewriteAbilityOptionWindow(int i) // �������ɷ� UI �׸���
    {
        switch (OptionsPlayerCanChoose[i][0])
        {
            case (int)Ablilty.hpUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "ü��\n<color=#92F4FF>20</color>���\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.damageUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "���ݷ�\n<color=#92F4FF>15</color>���\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.moveSpeedUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "�̵��ӵ�\n<color=#92F4FF>0.5</color>���\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.defenseUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "����\n<color=#92F4FF>5</color>���\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.criDamageUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "ġ��Ÿ������\n<color=#92F4FF>10</color>%���\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.criProbabilityUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "ġ��ŸȮ��\n<color=#92F4FF>5</color>%���\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.aovUp:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "������\n<color=#92F4FF>5</color>%���\n" + PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] + "/" + OptionsPlayerCanChoose[i][2];
                break;
            case (int)Ablilty.gold:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "���\n<color=#92F4FF>"+ (PlayerStatus.Instance.PlayerLevel+1)*50 +"</color>ȹ��";
                break;
            case (int)Ablilty.ricochetShot:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "�ٿ��\n<color=#92F4FF>�⺻������ ƨ��ϴ�.</color>";
                break;
            case (int)Ablilty.multiShot:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "��Ƽ��\n<color=#92F4FF>�⺻������ �ΰ��� �þ�ϴ�.</color>";
                break;
            case (int)Ablilty.chainShot:
                images[i].sprite = sprites[OptionsPlayerCanChoose[i][0]];
                Texts[i].text = "ü�μ�\n<color=#92F4FF>�⺻������ �ι��� �ߵ��մϴ�.</color>";
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
    public void Select(int i)  // ������ ������ ȿ��
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


            if (OptionsPlayerCanChoose[i][0] != (int)Ablilty.gold) // ���� ���ѷ����̱� ������ ��������� �������� ��������
            {
                PlayerStatus.Instance.Levelablilty[OptionsPlayerCanChoose[i][0]] += 1;
            }

            StartCoroutine(ShowSelcetedAbilityOptionWindow(i));
        }
    }
    IEnumerator ShowSelcetedAbilityOptionWindow(int i)  // �ð��� ȿ��
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
