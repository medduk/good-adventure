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
    [SerializeField] GameObject nowtalk;

    [SerializeField] int playerMaxHp = 100; //최대체력
    [SerializeField] int playerCurHp;       //현재체력
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

    bool Loadgame = false;      //이어하기 확인
    public bool textSkillOn = false; 
    public int textSkillLevel = 1;
    public int[] Levelablilty;      // 레벨업을 통한 능력습득상태
    [SerializeField] int[] runes;   // 영구상승 능력치 목록 , PlayerPrefs 의 저장기능을 이용하여 저장함

    public enum ShotSkills      // 스킬목록
    {
        ricochetShot,
        multiShot,
        chainShot,
        diagonalShot,   // 미구현
    }

    public int[] playerSkills;  // 스킬습득상태

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
    public float PlayerLevel
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

    public void GainExp(int _exp)   // 경험치습득 및 레벨업
    {
        playerCurExp += (int)(_exp * (1 - (runes[(int)Runes.exp] * 0.1f)));
        while(playerCurExp >= playerMaxExp)
        {
            playerCurExp -= playerMaxExp;
            playerEXPSlider.value = playerCurExp / playerMaxExp;
            playerLevel++;
            LVshow.text = "LV. " + playerLevel;
            playerMaxExp = (int)(playerMaxExp * 1.25f);
            gameObject.GetComponent<LevelAbility>().OpenUI();
            SoundManager.Instance.LevelUpSound.Play();
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

    private void InitiatePlayerStatus() // 게임을 첫 실행시 영구상승 능력을 적용하기 위함
    {
        /* Runes */
        runes = new int[System.Enum.GetValues(typeof(Runes)).Length];
        /* Skills */
        playerSkills = new int[System.Enum.GetValues(typeof(ShotSkills)).Length];   // 스킬을 초기화 함.

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
        Levelablilty = new int[System.Enum.GetNames(typeof(Ablilty)).Length];
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
        if (!Loadgame)  // 이어하기 인지 확인후, 이어하기가 맞다면 100% 체력셋팅과 골드지급을 하지 않음
        {
            inventory.instance.playerCoin = 0;
            inventory.instance.CoinText.text = "0";
            inventory.instance.GetOrGiveCoin(PlayerPrefs.GetInt("coin"));
            playerCurHp = playerMaxHp;
        }    
        playerHpSlider.value = 1f;  // Make Full Hp when the game gets started.
        LVshow.text = "LV. " + playerLevel;

        HPText();
    }

    private void Update()  // 체력바의 자연스러운 시각적 효과를 위해서 작성
    {
        playerHpSlider.value = Mathf.Lerp(playerHpSlider.value, (float)playerCurHp / playerMaxHp, Time.deltaTime * 5f);
        playerEXPSlider.value = Mathf.Lerp(playerEXPSlider.value, (float)playerCurExp / playerMaxExp, Time.deltaTime * 5f);
    }

    public void TakeDamage(int damage)
    {
        if (!stopDamage)
        {
            playerCurHp -= (int)(damage *(1- (float)playerDefense/(playerDefense+100)));
            // 100은 방어계수.

            SoundManager.Instance.playerHitSound.Play();

            animator.SetTrigger("IsHit");
            if (playerCurHp <= 0 && !isGameOver)
            {
                PlayerPrefs.SetInt("coin", inventory.instance.playerCoin/10);
                isGameOver = true;
                GameManager.Instance.SetGameOver();
                SoundManager.Instance.GameOverSound.Play();
                gameObject.GetComponent<moving>().SetGameOver();
                gameObject.GetComponent<MainFmAttack>().SetGameOver();
            }
            StartCoroutine(StopDamage());
        }
        HPText();
    }

    public void RevivalPlayer() // 게임 클리어 및 캐릭터가 죽어 재시작시 캐릭터 초기상태로 초기화용
    {
        isGameOver = false;
        gameObject.GetComponent<moving>().SetGameLiving();
        gameObject.GetComponent<MainFmAttack>().SetGameLiving();
        GameManager.Instance.SetGameLiving();
        Resetplay();
        playerCurHp = playerMaxHp;
        HPText();
        inventory.instance.playerCoin = 0;
        inventory.instance.CoinText.text = "0";
        inventory.instance.GetOrGiveCoin(PlayerPrefs.GetInt("coin"));
    }

    IEnumerator StopDamage()  // 데미지를 받고난후 무적시간
    {
        stopDamage = true;
        rigidbody2D.mass = rigidbody2D.mass * 10f;
        yield return new WaitForSeconds(1f);
        stopDamage = false;
        rigidbody2D.mass = rigidbody2D.mass * 0.1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)  // 각종 NPC 및 사물 상호작용, 본 게임에서는 원터치 진행을 선택하여 사용자의 편의성을 위해 접촉으로 모든 상호작용을 시도하게 하였음
    {
        if(collision.transform.name == "GameStartPortal")
        {
            SceneManager.LoadScene("SampleScene");
        }

        if (collision.transform.tag == "Portal")
        {
            StageManager.Instance.MoveNextStage();
        }

        if (collision.transform.tag == "ReinForce")
        {
            Destroy(collision.gameObject);
            GameManager.Instance.OpenReinForce();
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            if(collision.transform.tag == "Rune")
            {
                StartCoroutine(RuneNPCmeet(collision.gameObject));
            }
            if (collision.transform.tag == "BlackSmith")
            {
                StartCoroutine(BlackSmithNPCmeet(collision.gameObject));
            }
            if (collision.transform.tag == "SHOP")
            {
                StartCoroutine(SHOPNPCmeet(collision.gameObject));
            }
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
        HPshow.text = playerCurHp + "/" + playerMaxHp;
    }

    public void SaveGame() // 플랫폼마다의 특정 경로를 구하여 그 경로에다가 플레이어의 정보를 직렬화 하여 bin파일을 생성하여 저장
    {
        SaveData save = new SaveData();
        save.PlayerMaxHp = PlayerMaxHp;
        save.playerCurHp = playerCurHp;
        save.playerMaxExp = playerMaxExp;
        save.playerCurExp = playerCurExp;
        save.playerLevel = playerLevel;

        save.Coin = inventory.instance.playerCoin;

        for(int k = 0; k < Levelablilty.Length; k++)
        {
            save.Levelablilty[k] = Levelablilty[k];
        }

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

    public void LoadGame() // 플랫폼마다의 특정 경로를 구하여 그 경로에 저장된 플레이어의 정보를 역직렬화 하여 불러옴
    {
        Loadgame = true;
        SaveData save = SaveManager.Load();
        PlayerMaxHp = save.PlayerMaxHp;
        playerCurHp = save.playerCurHp;
        playerMaxExp = save.playerMaxExp;
        playerCurExp = save.playerCurExp;
        playerLevel = save.playerLevel;



        transform.position = new Vector3(save.x, save.y, save.z);

        LVshow.text = "LV. " + playerLevel;
        HPText();

        for (int k = 0; k < Levelablilty.Length; k++)
        {
            Levelablilty[k] = save.Levelablilty[k];
        }
        playerDamage += Levelablilty[1] * 15;
        playerMoveSpeed += Levelablilty[2] * 0.5f;
        playerDefense += Levelablilty[3] * 5;
        criticalDamage += Levelablilty[4] * 10;
        criticalProbability += Levelablilty[5] * 5;
        absorptionOfVitality += Levelablilty[6] * 0.05f;
        playerSkills[0] = Levelablilty[8];
        playerSkills[1] = Levelablilty[9];
        playerSkills[2] = Levelablilty[10];


        for (int i = 0; i < save.equip.Count; i++)
        {
            inventory.instance.AddItem(ItemBundle.instance.MakeItem(save.equip[i]));
            inventory.instance.EquipItem(inventory.instance.items[0]);
            inventory.instance.RemoveItem(0);
        }
        for (int i = 0; i < save.items.Count; i++)
        {
            inventory.instance.AddItem(ItemBundle.instance.MakeItem(save.items[i]));
        }
        inventory.instance.GetOrGiveCoin(save.Coin);
    }
    public void Resetplay()
    {
        inventory.instance.items.Clear();

        for (int i = 0; i < inventory.instance.equip.Count; i++) 
        {
            inventory.instance.equip[i].UnUse();
            
        }
        inventory.instance.equip.Clear();
        inventory.instance.onChangeItem.Invoke();
        inventory.instance.onChangeEquip.Invoke();

        playerMaxHp -= Levelablilty[0] * 20;
        playerDamage -= Levelablilty[1] * 15;
        playerMoveSpeed -= Levelablilty[2] * 0.5f;
        playerDefense -= Levelablilty[3] * 5;
        criticalDamage -= Levelablilty[4] * 10;
        criticalProbability -= Levelablilty[5] * 5;
        absorptionOfVitality -= Levelablilty[6] * 0.05f;

        for(int i = 0; i< Levelablilty.Length; i++)
        {
            if( i != (int)Ablilty.gold)
                Levelablilty[i] = 0;
        }
        for (int i = 0; i < playerSkills.Length; i++)
        {
            playerSkills[i] = 0;
        }
        playerLevel = 1;
        LVshow.text = "LV. " + playerLevel;
        playerMaxExp = 100;
        playerCurExp = 0;
    }

    IEnumerator RuneNPCmeet(GameObject NPC)
    {
        dialogManager.Action(NPC);
        GameManager.Instance.Who = NPC;
        Vector3 P = NPC.transform.position;
        P.y = P.y + 1.5f;
        gameObject.transform.position = P;
        while (nowtalk.activeSelf)
        {
            yield return null;
        }
        GameManager.Instance.OpenRune();

    }
    IEnumerator BlackSmithNPCmeet(GameObject NPC)
    {
        dialogManager.Action(NPC);
        GameManager.Instance.Who = NPC;
        Vector3 P = NPC.transform.position;
        P.x = P.x - 1.5f;
        gameObject.transform.position = P;
        while (nowtalk.activeSelf)
        {
            yield return null;
        }
        GameManager.Instance.OpenReinForce();

    }
    IEnumerator SHOPNPCmeet(GameObject NPC)
    {
        dialogManager.Action(NPC);
        GameManager.Instance.Who = NPC;
        Vector3 P = NPC.transform.position;
        P.x = P.x + 1.5f;
        gameObject.transform.position = P;
        while (nowtalk.activeSelf)
        {
            yield return null;
        }
        GameManager.Instance.OpenSHOP();

    }
}
