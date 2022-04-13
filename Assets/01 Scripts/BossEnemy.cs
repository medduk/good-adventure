using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossEnemy : MonoBehaviour
{
    [SerializeField] string iDName;

    public Animator animator;
    private new Rigidbody2D rigidbody2D;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;

    [SerializeField] Slider enemyHpSlider;
    [SerializeField] Slider BackenemyHpSlider;

    [SerializeField] int enemyMaxHp = 500;
    [SerializeField] int enemyHp;
    [SerializeField] float enemyMoveSpeed = 0f;
    [SerializeField] int enemyDamage = 20;
    [SerializeField] int enemyGiveExp = 30;

    [SerializeField] int[] dropItemId;
    [SerializeField] int[] dropIChance;
    private int sum = 0;
    List<Enemy> enemyFriends = new List<Enemy>(); // 일점범위내 팀이 맞을때 감지
    List<BossEnemy> enemyBosses = new List<BossEnemy>();

    private bool backHpswitch = false;
    private bool playerCheck = false;
    private bool stopMove = false;
    private GameObject player;
    private Vector3 playerDir;

    public Sprite Icon;

    public delegate void HitPattern();
    public HitPattern hitPattern;
    public delegate void DiePattern();
    public DiePattern diePattern;

    public string IDName
    {
        get
        {
            return iDName;
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        gameObject.transform.parent = GameObject.Find("EnemyManager").transform;
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

        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (collision.transform.tag == "Boss") //보스라면
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


    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerStatus.Instance.TakeDamage(enemyDamage);
        }

    }
    public void FriendHit()
    {
        playerCheck = true;

    }
    private void FixedUpdate()
    {
        if (playerCheck && !stopMove)
        {
            playerDir = player.transform.position - transform.position;
            if (playerDir.x > 0)
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
        GameManager.Instance.OpenBossHpbar();
        GameManager.Instance.changeBossIcon(Icon);
        enemyHpSlider = GameObject.Find("BossEnemyHPCanvas").GetComponent<BossHPbar>().GetBossHP;
        BackenemyHpSlider = GameObject.Find("BossEnemyHPCanvas").GetComponent<BossHPbar>().GetBossHPEffect;
        enemyHp = enemyMaxHp;
        enemyHpSlider.value = 1f; // 풀 HP
        BackenemyHpSlider.value = 1f;

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
    public void TakeDamage((int damage, bool isCritical) _damage)
    {
        DamageTextManager.Instance.DisplayDamage(_damage.damage, transform.position, _damage.isCritical);   // Damage Text
        enemyHpSlider.value = (float)enemyHp / enemyMaxHp;
        BackenemyHpSlider.value = enemyHpSlider.value;
        enemyHp -= _damage.damage;
        GameManager.Instance.changeBossIcon(Icon);
        Hpbar();
        playerCheck = true;



        PlayerStatus.Instance.AbsorbHp(_damage.damage);

        for (int i = 0; i < enemyFriends.Count; i++)
        {
            enemyFriends[i].FriendHit();
        }
        for (int i = 0; i < enemyBosses.Count; i++)
        {
            enemyBosses[i].FriendHit();
        }
        if (enemyHp <= 0)
        {
            gameObject.transform.localScale = new Vector3(1f, 1f);
            animator.SetTrigger("bossdie");
            circleCollider2D.enabled = false;
            if (diePattern != null) diePattern.Invoke();
            stopMove = true;
            rigidbody2D.velocity = Vector2.zero;
            PlayerStatus.Instance.GainExp(enemyGiveExp);
        }
        else
        {
            animator.SetTrigger("bosshit");
            if (hitPattern != null) hitPattern.Invoke(); // 피격시 패턴 발동
            StartCoroutine(StopMove());
        }
    }

    public void Hpbar()
    {
        enemyHpSlider.value = (float)enemyHp / enemyMaxHp;
        StartCoroutine(BackHpRun());
    }


    private void DieEnemy()
    {
        GameManager.Instance.CloseBossHpbar();
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
}
