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


    [SerializeField] int enemyMaxHp = 100;
    [SerializeField] int enemyHp;
    [SerializeField] float enemyMoveSpeed = 10f;
    [SerializeField] int enemyDamage = 10;
    [SerializeField] int enemyGiveExp = 30;
    [SerializeField] int enemyGiveCoin = 30;

    [SerializeField] int[] dropItemId;
    [SerializeField] int[] dropIChance;
    private int sum = 0;
    List<Enemy> enemyFriends = new List<Enemy>(); // 일점범위내 팀이 맞을때 감지
    List<BossEnemy> enemyBosses = new List<BossEnemy>();
    static WaitForSeconds sec;

    private bool backHpswitch = false;
    private bool playerCheck = false;
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
    private void OnTriggerEnter2D(Collider2D collision)
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
    private void OnTriggerExit2D(Collider2D collision)
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
    public void FriendHit()
    {
        playerCheck = true;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStatus.Instance.TakeDamage(enemyDamage);
        }

    }
    private void FixedUpdate()
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
    private void Update()
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
        DamageTextManager.Instance.DisplayDamage(_damage.damage, transform.position,_damage.isCritical);   // Damage Text

        enemyHp -= _damage.damage;
        enemyHpSlider.value = (float)enemyHp / enemyMaxHp;
        ImporUnitHP.sizeDelta = new Vector2(Sildershow.sizeDelta.x * enemyHpSlider.value, ImporUnitHP.sizeDelta.y);
        ImporUnitHP.anchoredPosition = new Vector2((Sildershow.sizeDelta.x - ImporUnitHP.sizeDelta.x)/-2, 0);
        StartCoroutine(BackHpRun());
        playerCheck = true;

        PlayerStatus.Instance.AbsorbHp(_damage.damage);

        for(int i = 0; i< enemyFriends.Count; i++)
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

    private IEnumerator DieEnemy()
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

    IEnumerator StopMove()
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
    private void HpBarUnit()
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
