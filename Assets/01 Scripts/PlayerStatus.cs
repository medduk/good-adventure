using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] Slider playerHpSlider;
    [SerializeField] Slider playerEXPSlider;
    [SerializeField] Text HPshow;
    [SerializeField] Text LVshow;

    [SerializeField] int playerMaxHp = 100;
    [SerializeField] int playerCurHp;
    [SerializeField] int playerDamage = 10;     // 공격력
    [SerializeField] float playerMoveSpeed = 2f;    // 이속
    [SerializeField] float playerAttackDelay = 1f;    // 실제공속
    [SerializeField] float playerChangeAttackDelay =1f; // 누적공속

    [SerializeField] int playerDefense;       // 방어력
    [SerializeField] float criticalDamage = 0.5f;      // 치명타 데미지
    [SerializeField] float criticalProbability = 1f; // 치명타 확률
    [SerializeField] float absorptionOfVitality = 0f;    // 생명력 흡수
    [SerializeField] int playerMaxExp = 100;   // 모아야할 경험치
    [SerializeField] float playerCurExp;   // 현재 경험치

    [SerializeField] float playerLevel = 1;   // 현재 레벨

    public bool textSkillOn = false;
    public int textSkillLevel = 1;
    public int[] Levelablilty = new int[System.Enum.GetNames(typeof(Ablilty)).Length -1];
    [SerializeField] int[] runes;

    public enum ShotSkills
    {
        ricochetShot,
        multiShot,
        chainShot,
        diagonalShot,
    }

    public int[] playerSkills;

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
    public int PlayerCurHp
    {
        set
        {
            playerCurHp = value;
        }
        get
        {
            return playerCurHp;
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

    public float PlayerChangeAttackDelay
    {
        set
        {
            playerChangeAttackDelay = value;
            if (playerChangeAttackDelay <= 0.2f)
            {
                playerAttackDelay = 0.2f;
            }
            else
                playerAttackDelay = playerChangeAttackDelay;
        }
        get
        {

            return playerChangeAttackDelay;
        }
    }
    public float PlayerAttackDelay
    {
        get
        {
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
    private float PlayerCurExp
    {
        set
        {
            playerCurExp = value;
        }
        get
        {
            return playerCurExp;
        }
    }
    private float PlayerLevel
    {
        set
        {
            playerLevel = value;
        }
        get
        {
            return playerLevel;
        }
    }

    public void GainExp(int _exp)
    {
        playerCurExp += (int)(_exp * (1 - (runes[(int)Runes.exp] * 0.1f)));
        while(playerCurExp >= playerMaxExp)
        {
            playerCurExp -= playerMaxExp;
            playerEXPSlider.value = playerCurExp / playerMaxExp;
            playerLevel++;
            LVshow.text = "LV. " + playerLevel;
            playerMaxExp = (int)(playerMaxExp * 1.25f);
            gameObject.GetComponent<LevelAblilty>().openUI();
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
        /* Skills */
        playerSkills = new int[System.Enum.GetValues(typeof(ShotSkills)).Length];

        for (int i=0; i < runes.Length; i++)
        {
            runes[i] = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), i));
        }

        playerMaxHp += (runes[(int)Runes.hp] * 2);
        playerDamage += runes[(int)Runes.damage];
        PlayerChangeAttackDelay -= runes[(int)Runes.attackDelay] * 0.01f;
        playerMoveSpeed += runes[(int)Runes.moveSpeed] * 0.02f;
        playerDefense += runes[(int)Runes.defense];
        criticalDamage += (runes[(int)Runes.criDamage]);
        criticalProbability += (runes[(int)Runes.criProbability] * 0.5f);
        absorptionOfVitality += (runes[(int)Runes.aov] * 0.005f);
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
        /*For Text*/
        if (textSkillOn)
        {
            playerSkills[(int)ShotSkills.multiShot] = textSkillLevel;
            playerSkills[(int)ShotSkills.chainShot] = textSkillLevel;
        }

        playerCurHp = playerMaxHp;
        playerHpSlider.value = 1f;  // Make Full Hp when the game gets started.
        LVshow.text = "LV. " + playerLevel;
        HPText();
    }

    private void Update()
    {
        playerHpSlider.value = Mathf.Lerp(playerHpSlider.value, (float)playerCurHp / playerMaxHp, Time.deltaTime * 5f);
        playerEXPSlider.value = Mathf.Lerp(playerEXPSlider.value, (float)playerCurExp / playerMaxExp, Time.deltaTime * 5f);

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
        HPText();
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
            Destroy(collision.gameObject);
            dialogManager.Action(collision.gameObject);
        }
        if (collision.transform.tag == "ReinForce")
        {
            Destroy(collision.gameObject);
            GameManager.Instance.OpenReinForce();
        }
    }

    public void GetRune(int runeIndex)
    {
        runes[runeIndex]++;
        PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), runeIndex),runes[runeIndex]);
    }

    public void RecoveryHp(int _hp)
    {
        playerCurHp += _hp;
        if (playerCurHp > playerMaxHp)
        {
            playerCurHp = playerMaxHp;
        }
        HPText();
    }

    public void AbsorbHp(int _damage)
    {
        RecoveryHp((int)(_damage * absorptionOfVitality));
    }

    public (int ,bool) CalPlayerDamage(float _percent = 1f,bool _canCri = true)
    {
        float damage = this.playerDamage * _percent;
        bool isCritical = false;

        float critical = Random.Range(0, 100f);

        if(critical <= criticalProbability && _canCri)
        {
            damage = this.playerDamage * (1 + criticalDamage/100);
            isCritical = true;
        }

        return ((int)damage,isCritical);
    }
    public void HPText() // 체력 수치 표시 함수
    {
        HPshow.text = playerCurHp + " / " + playerMaxHp;
    }

    public void SaveGame()
    {
        SaveData save = new SaveData();
        save.PlayerMaxHp = PlayerMaxHp;
        save.playerCurHp = playerCurHp;
        save.playerMaxExp = playerMaxExp;
        save.playerCurExp = playerCurExp;
        save.playerLevel = playerLevel;

        save.x = transform.position.x;
        save.y = transform.position.y;
        save.z = transform.position.z;

        for (int i = 0; i < inventory.instance.equip.Count; i++)
        {

            save.equip.Add(inventory.instance.equip[i].itemID);
        }

        for (int i = 0; i < inventory.instance.items.Count; i++)
        {
            save.items.Add(inventory.instance.items[i].itemID);
        }

        SaveManager.Save(save);
    }

    public void LoadGame()
    {
        SaveData save = SaveManager.Load();
        PlayerMaxHp = save.PlayerMaxHp;
        playerCurHp = save.playerCurHp;
        playerMaxExp = save.playerMaxExp;
        playerCurExp = save.playerCurExp;
        playerLevel = save.playerLevel;

        transform.position = new Vector3(save.x, save.y, save.z);

        LVshow.text = "LV. " + playerLevel;
        HPText();

        for (int i = 0; i < save.equip.Count; i++)
        {
            inventory.instance.AddItem(ItemBundle.instance.makeItem(save.equip[i]));
            inventory.instance.EquipItem(inventory.instance.items[0]);
            inventory.instance.RemoveItem(0);
        }
        for (int i = 0; i < save.items.Count; i++)
        {
            inventory.instance.AddItem(ItemBundle.instance.makeItem(save.items[i]));
        }

    }
    public void runeReset()
    {
        for (int i = 0; i < runes.Length; i++)
        {
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), i),0);
        }
    }
}
