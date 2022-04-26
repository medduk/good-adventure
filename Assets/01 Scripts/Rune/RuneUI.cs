using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RuneUI : MonoBehaviour
{
    public GameObject Runepage;
    public GameObject RuneExplanationpage;
    public Button button;
    public Sprite what, wow;
    public TextMeshProUGUI Count;

    bool IsRunepage = true;
    int runestoneCount = 5;
    int level;


    public int RunestoneCount
    {
        get
        {
            return runestoneCount;
        }
    }

    // Start is called before the first frame update
    public static RuneUI instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        runestoneCountchange();
    }
    public void ChangePage()
    {
        
        if (IsRunepage)
        {
            button.image.sprite = wow;
            Runepage.SetActive(false);
            RuneExplanationpage.SetActive(true);
            IsRunepage = false;
            return;
        }
        if (!IsRunepage)
        {
            button.image.sprite = what;
            Runepage.SetActive(true);
            RuneExplanationpage.SetActive(false);
            IsRunepage = true;
            return;
        }
    }
    public void Levelup(Runes runes)
    {
        if(runestoneCount > 0)
        {
            level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), runes));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), runes), level+1);
            AddPower(runes);
            runestoneCount--;
            runestoneCountchange();
        }
    }
    void runestoneCountchange()
    {
        Count.text = +runestoneCount + "";

        if(runestoneCount > 99)
        {
            Count.text = "99+";
        }
    }
    public void AddPower(Runes rune)
    {
        switch (rune)
        {
            case Runes.hp:
                {
                    PlayerStatus.Instance.PlayerMaxHp += 2;
                    PlayerStatus.Instance.HPText();
                    break;
                }
            case Runes.damage:
                {
                    PlayerStatus.Instance.PlayerDamage += 1;
                    break;
                }
            case Runes.attackDelay:
                {
                    PlayerStatus.Instance.PlayerChangeAttackDelay -= 0.01f;
                    break;
                }
            case Runes.moveSpeed:
                {
                    PlayerStatus.Instance.PlayerMoveSpeed += 0.02f;
                    break;
                }
            case Runes.defense:
                {
                    PlayerStatus.Instance.PlayerDefense += 1;
                    break;
                }
            case Runes.criDamage:
                {
                    PlayerStatus.Instance.CriticalDamage += 1;
                    break;
                }
            case Runes.criProbability:
                {
                    PlayerStatus.Instance.CriticalProbability += 0.5f;
                    break;
                }
            case Runes.aov:
                {
                    PlayerStatus.Instance.AbsorptionOfVitality += 0.005f;
                    break;
                }
        }
    }

}
