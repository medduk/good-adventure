using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] Slider playerHpSlider;

    [SerializeField] int playerMaxHp = 100;
    [SerializeField] int playerCurHp;
    [SerializeField] int playerDamage = 10;     // 공격력
    [SerializeField] float playerMoveSpeed = 2f;    // 이속
    [SerializeField] float playerAttackDelay = 1f;    // 공속

    [SerializeField] int playerDefense;       // 방어력
    [SerializeField] float criticalDamage = 0.5f;      // 치명타 데미지
    [SerializeField] float criticalProbability = 1f; // 치명타 확률
    [SerializeField] float absorptionOfVitality = 0f;    // 생명력 흡수
    [SerializeField] int playerMaxExp = 100;   // 모아야할 경험치
    [SerializeField] float playerCurExp;   // 현재 경험치

    [SerializeField] float playerLevel = 1;   // 현재 경험치

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

    [SerializeField] int[] runes;

    private bool stopDamage = false;

    private static PlayerStatus instance = null;
    private Animator animator;
    private new Rigidbody2D rigidbody2D;

    private bool isGameOver = false;

    public DialogManager dialogManager;

    public int PlayerMaxHp
    {
        set
        {
            playerMaxHp = value;
        }
        get
        {
            return playerMaxHp;
        }
    }

    public int PlayerDamage
    {
        set
        {
            playerDamage = value;
        }
        get
        {
            return playerDamage;
        }
    }
    public float PlayerMoveSpeed
    {
        set 
        {
            playerMoveSpeed = value;
        }
        get
        {
            return playerMoveSpeed;
        }
    }

    public float PlayerAttackDelay
    {
        set
        {
            playerAttackDelay = value;
        }
        get
        {
            if(playerAttackDelay <= 0.2f)
            {
                return 0.2f;
            }
            return playerAttackDelay;
        }
    }
    public int PlayerDefense
    {
        set
        {
            playerDefense = value;
        }
        get
        {
            return playerDefense;
        }
    }

    public float CriticalDamage
    {
        set
        {
            criticalDamage = value;
        }
        get
        {
            return criticalDamage;
        }
    }

    public float CriticalProbability
    {
        set
        {
            criticalProbability = value;
        }
        get
        {
            return criticalProbability;
        }
    }

    public float AbsorptionOfVitality
    {
        set
        {
            absorptionOfVitality = value;
        }
        get
        {
            return absorptionOfVitality;
        }
    }
    
    private int PlayerMaxExp
    {
        set
        {
            playerMaxExp = value;
        }
        get
        {
            return playerMaxExp;
        }
    }

    public void GainExp(int _exp)
    {
        playerCurExp += (int)(_exp * (1 - (runes[(int)Runes.exp] * 0.1f)));
        while(playerCurExp >= playerMaxExp)
        {
            playerCurExp -= playerMaxExp;

            playerLevel++;
            playerMaxExp = (int)(playerMaxExp * 1.25f);
        }
    }

    public static PlayerStatus Instance
    {
        get
        {
            if (instance != null) return instance;
            return null;
        }
    }

    private void InitiatePlayerStatus() 
    {
        /* Runes */
        runes = new int[System.Enum.GetValues(typeof(Runes)).Length];

        for(int i=0; i < runes.Length; i++)
        {
            runes[i] = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), i));
        }

        playerMaxHp += (runes[(int)Runes.hp] * 2);
        playerDamage += runes[(int)Runes.damage];
        playerAttackDelay -= runes[(int)Runes.attackDelay] * 0.01f;
        playerMoveSpeed += runes[(int)Runes.attackDelay] * 0.02f;
        playerDefense += runes[(int)Runes.defense];
        criticalDamage += (runes[(int)Runes.criDamage] * 0.02f);
        criticalProbability += (runes[(int)Runes.criProbability] * 0.5f);
        absorptionOfVitality += (runes[(int)Runes.aov] * 0.05f);
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitiatePlayerStatus();

        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        isGameOver = false;
    }
    private void Start()
    {
        playerCurHp = playerMaxHp;
        playerHpSlider.value = 1f;  // Make Full Hp when the game gets started.

        Debug.Log(PlayerMaxExp);
    }

    private void Update()
    {
        playerHpSlider.value = Mathf.Lerp(playerHpSlider.value, (float)playerCurHp / playerMaxHp, Time.deltaTime * 5f);
    }
    public void TakeDamage(int damage)
    {
        if (!stopDamage)
        {
            playerCurHp -= (int)(damage *(1- playerDefense/(playerDefense+100)));

            animator.SetTrigger("IsHit");
            if (playerCurHp <= 0 && !isGameOver)
            {
                isGameOver = true;
                GameManager.Instance.SetGameOver();
                gameObject.GetComponent<moving>().SetGameOver();
                gameObject.GetComponent<MainFmAttack>().SetGameOver();
            }
            StartCoroutine(StopDamage());
        }
    }
    IEnumerator StopDamage()
    {
        stopDamage = true;
        rigidbody2D.mass = rigidbody2D.mass * 10f;
        yield return new WaitForSeconds(1f);
        stopDamage = false;
        rigidbody2D.mass = rigidbody2D.mass * 0.1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "GameStartPortal")
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (collision.transform.tag == "Portal")
        {
            StageManager.Instance.MoveNextStage();
        }

        if (collision.transform.tag == "Tutorial")
        {
           
            Destroy(collision);
            dialogManager.Action(collision.gameObject);
        }
    }

    public void GetRune(int runeIndex)
    {
        runes[runeIndex]++;
        PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), runeIndex),runes[runeIndex]);
    }

    public void AbsorbHp(int _damage)
    {
        playerCurHp += (int)(_damage * absorptionOfVitality);
    }

    public int CalPlayerDamage()
    {
        int damage = playerDamage;

        float critical = Random.Range(0, 100f);

        if(critical <= criticalProbability)
        {
            damage = (int)(playerDamage * (1 + criticalDamage/100));
        }

        Debug.Log("Damage:" + damage);

        return damage;
    }
}
