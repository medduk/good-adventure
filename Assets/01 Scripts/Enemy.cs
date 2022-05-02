using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] string iDName;

    private int unitHp = 200;

    public Animator animator;
    private new Rigidbody2D rigidbody2D;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;
    private RectTransform ImporUnitHP;
    private RectTransform Sildershow;

    [SerializeField] Slider enemyHpSlider;
    [SerializeField] Slider BackenemyHpSlider;
    [SerializeField] GameObject UnitHpshow;


    [SerializeField] int enemyMaxHp = 100; // 몬스터 최대체력
    [SerializeField] int enemyHp;  // 몬스터 현제체력
    [SerializeField] float enemyMoveSpeed = 10f; // 몬스터 이동속도
    [SerializeField] int enemyDamage = 10; // 몬스터 데미지
    [SerializeField] int enemyGiveExp = 30; // 처치시 지급경험치
    [SerializeField] int enemyGiveCoin = 30; // 처치시 지급골드

    [SerializeField] int[] dropItemId;  // 아이템 드랍테이블
    [SerializeField] int[] dropIChance; // 아이템 드랍확률
    private int sum = 0;  // 아이템 확률계산용

    /* 일정범위내 팀이 공격받을때 감지하기 위한 주변몬스터 등록리스트 */
    List<Enemy> enemyFriends = new List<Enemy>(); 
    List<BossEnemy> enemyBosses = new List<BossEnemy>();

    static WaitForSeconds sec;  // 피격시 경직길이

    private bool backHpswitch = false;
    private bool playerCheck = false;  // 플레이어 확인여부
    private bool stopMove = false;
    public bool cangiveItem = true;
    private GameObject player;
    private Vector3 playerDir;

    public string IDName
    {
        get
        {
            return iDName;
        }
    }
    public bool PlayerCheck
    {
        get
        {
            return playerCheck;
        }
        set
        {
            playerCheck = value;
        }
    }
    public int EnemyMaxHp
    {
        get
        {
            return enemyMaxHp;
        }
        set
        {
            enemyMaxHp = value;
        }
    }
    public int EnemyHp
    {
        get
        {
            return enemyHp;
        }
        set
        {
            enemyHp = value;
        }
    }
    public float EnemyMoveSpeed
    {
        get
        {
            return enemyMoveSpeed;
        }
        set
        {
            enemyMoveSpeed = value;
        }
    }
    public int EnemyDamage
    {
        get
        {
            return enemyDamage;
        }
        set
        {
            enemyDamage = value;
        }
    }
    public int EnemyGiveExp
    {
        get
        {
            return enemyGiveExp;
        }
        set
        {
            enemyGiveExp = value;
        }
    }
    public int EnemyGiveCoin
    {
        get
        {
            return enemyGiveCoin;
        }
        set
        {
            enemyGiveCoin = value;
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();

        for (int c = 0; c < dropItemId.Length; c++)
        {
            sum += dropIChance[c];
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)  // 주변의 같은 몬스터 등록
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {   
            playerCheck = true;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.transform.tag == "Boss") //보스가 아니라면
                enemyBosses.Add(collision.gameObject.GetComponent<BossEnemy>());
            else
                enemyFriends.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // 주변의 같은 몬스터 사망시 리스트에서 제거
    {
        if (enemyFriends.Contains(collision.gameObject.GetComponent<Enemy>()))
        {


            enemyFriends.Remove(collision.gameObject.GetComponent<Enemy>());
        }
        if (enemyBosses.Contains(collision.gameObject.GetComponent<BossEnemy>()))
        {


            enemyBosses.Remove(collision.gameObject.GetComponent<BossEnemy>());
        }

    }
    public void FriendHit()  // 주변 몬스터가 피격시 플레이어를 인지하게만듬
    {
        playerCheck = true;
    }

    private void OnCollisionStay2D(Collision2D collision)  // 몬스터의 기본공격방식, 접촉으로 공격함
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStatus.Instance.TakeDamage(enemyDamage);
        }

    }
    private void FixedUpdate()  // 움직임과 관련된 함수, 플레이어를 인지하였으면 공격하기위해 플레이어위치로 이동함, 플레이어의 이동함수와 유사
    {
        if (playerCheck && !stopMove)
        {
            animator.SetBool("IsWalking", true);
            playerDir = player.transform.position - transform.position;
            if(playerDir.x > 0)
            {
                spriteRenderer.flipX = true;
            }
            if (playerDir.x < 0)
            {
                spriteRenderer.flipX = false;
            }
            //rigidbody2D.velocity = playerDir.normalized * enemyMoveSpeed * Time.fixedDeltaTime;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * (enemyMoveSpeed * 0.1f));
        }
        
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHp = enemyMaxHp;
        enemyHpSlider.value = 1f; // 풀 HP
        BackenemyHpSlider.value = 1f;
        ImporUnitHP = UnitHpshow.GetComponent<RectTransform>();
        Sildershow = enemyHpSlider.GetComponent<RectTransform>();
        HpBarUnit();
    }
    private void Update() // 데미지를 받았을때 데미지 만큼의 하얀색의 영역을 보여줘서 강함을 느낄 수 있게 함, 타격감을 위하여 이렇게 구현  
    {
        if (backHpswitch)
        {
            BackenemyHpSlider.value = Mathf.Lerp(BackenemyHpSlider.value, (float)enemyHp / enemyMaxHp, Time.deltaTime * 10f);
            if (enemyHpSlider.value >= BackenemyHpSlider.value - 0.01f)
            {
                backHpswitch = false;
                BackenemyHpSlider.value = enemyHpSlider.value;
            }
        }
    }
    public void TakeDamage((int damage,bool isCritical) _damage)
    {
        SoundManager.Instance.enemyHitSound.Play();

        DamageTextManager.Instance.DisplayDamage(_damage.damage, transform.position,_damage.isCritical);   // Damage Text

        enemyHp -= _damage.damage;
        enemyHpSlider.value = (float)enemyHp / enemyMaxHp;
        ImporUnitHP.sizeDelta = new Vector2(Sildershow.sizeDelta.x * enemyHpSlider.value, ImporUnitHP.sizeDelta.y);
        ImporUnitHP.anchoredPosition = new Vector2((Sildershow.sizeDelta.x - ImporUnitHP.sizeDelta.x)/-2, 0);
        StartCoroutine(BackHpRun());
        playerCheck = true;

        PlayerStatus.Instance.AbsorbHp(_damage.damage);

        for(int i = 0; i< enemyFriends.Count; i++) // 데미지를 받았을때 주변 몬스터에게 플레이어를 인지시킴
        {
            enemyFriends[i].FriendHit();
        }
        for (int i = 0; i < enemyBosses.Count; i++)
        {
            enemyBosses[i].FriendHit();
        }
        if (enemyHp <= 0) StartCoroutine(DieEnemy());
        else
        {
            animator.SetTrigger("IsHit");
            StartCoroutine(StopMove());
        }
    }

    private IEnumerator DieEnemy()  // 죽을 때 발동
    {
        animator.SetTrigger("IsDie");

        circleCollider2D.enabled = false;
        enemyHpSlider.gameObject.SetActive(false);

        stopMove = true;
        rigidbody2D.velocity = Vector2.zero;

        PlayerStatus.Instance.GainExp(enemyGiveExp);
 
        sec = new WaitForSeconds(0.3f);
        yield return sec;
        int DropIndex = DropItem();
        if (cangiveItem) // 만약 아이템을 주는 몬스터 라면
        { 
            ItemBundle.instance.Drop(transform.position, dropItemId[DropIndex]);
            inventory.instance.GetOrGiveCoin(enemyGiveCoin);
        }


        MainFmAttack.Instance.RemoveDeadEnemy(gameObject);
        Destroy(gameObject);      
    }

    IEnumerator StopMove() // 데미지 받고 경직효과
    {
        stopMove = true;
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        stopMove = false;
    }

    IEnumerator BackHpRun()
    {
        yield return new WaitForSeconds(0.1f);
        backHpswitch = true;
    }
    private void HpBarUnit()  // 기타 게임처럼 체력이 많을수록 시작적으로 체력바의 이미지가 빼곡해져서 체력이 많아보임을 인지가능
    {
        float scaleX = 1.5f / ((float)enemyMaxHp / (float)unitHp);
        UnitHpshow.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        foreach (Transform child in UnitHpshow.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }
        UnitHpshow.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
    private int DropItem()
    {
        int randomIndex = Random.Range(1, sum + 1);

        int i = 0;
        while (i < dropItemId.Length)
        {
            randomIndex = randomIndex - dropIChance[i];
            if (randomIndex <= 0)
            {
                break;
            }
            i++;
        }
        return i;
    }
    public void TriggerDie()
    {
        circleCollider2D.enabled = false;
        StartCoroutine(DieEnemy());
    }
}
