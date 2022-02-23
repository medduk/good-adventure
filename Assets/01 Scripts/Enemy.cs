using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private int unitHp = 50; 

    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    private CircleCollider2D circleCollider2D;
    private SpriteRenderer spriteRenderer;
    private RectTransform ImporUnitHP;
    private RectTransform Sildershow;

    [SerializeField] Slider enemyHpSlider;
    [SerializeField] Slider BackenemyHpSlider;
    [SerializeField] GameObject UnitHpShow;


    [SerializeField] int enemyMaxHp = 100;
    [SerializeField] int enemyHp;
    [SerializeField] float enemyMoveSpeed = 10f;
    [SerializeField] int enemyDamage = 10;

    List<Enemy> enemyFriends = new List<Enemy>(); // 일정범위내 팀이 맞을때 감지

    static WaitForSeconds sec;

    private bool backHpswidth = false;
    private bool playerCheck = false;
    private bool stopMove = false;
    private GameObject player;
    private Vector3 playerDir;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        circleCollider2D = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {   
            playerCheck = true;
            
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyFriends.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemyFriends.Contains(collision.gameObject.GetComponent<Enemy>()))
        {
            enemyFriends.Remove(collision.gameObject.GetComponent<Enemy>());
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
            rigidbody2D.velocity = Vector2.zero;
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
            rigidbody2D.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * (enemyMoveSpeed * 0.1f));
            //rigidbody2D.velocity = (player.transform.position - transform.position) * Time.deltaTime * enemyMoveSpeed;
        }
    }
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyHp = enemyMaxHp;
        enemyHpSlider.value = 1f; // 풀 HP
        BackenemyHpSlider.value = 1f;
        ImporUnitHP = UnitHpShow.GetComponent<RectTransform>();
        Sildershow = enemyHpSlider.GetComponent<RectTransform>();
        HpBarUnit();
    }
    private void Update()
    {
        if (backHpswidth)
        {
            BackenemyHpSlider.value = Mathf.Lerp(BackenemyHpSlider.value, (float)enemyHp / enemyMaxHp, Time.deltaTime * 10f);
            if (enemyHpSlider.value >= BackenemyHpSlider.value - 0.01f)
            {
                backHpswidth = false;
                BackenemyHpSlider.value = enemyHpSlider.value;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        enemyHp -= damage;
        enemyHpSlider.value = (float)enemyHp / enemyMaxHp;
        ImporUnitHP.sizeDelta = new Vector2(Sildershow.sizeDelta.x * enemyHpSlider.value, ImporUnitHP.sizeDelta.y);
        ImporUnitHP.anchoredPosition = new Vector2((Sildershow.sizeDelta.x - ImporUnitHP.sizeDelta.x)/-2, 0);
        StartCoroutine(BackHpRun());
        playerCheck = true;

        for(int i = 0; i< enemyFriends.Count; i++)
        {
            enemyFriends[i].FriendHit();
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

        sec = new WaitForSeconds(0.3f);
        yield return sec;
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
        backHpswidth = true;
    }
    private void HpBarUnit()
    {
        float scaleX = 5.0f / ((float)enemyMaxHp / (float)unitHp);
        UnitHpShow.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(false);
        foreach (Transform child in UnitHpShow.transform)
        {
            child.gameObject.transform.localScale = new Vector3(scaleX, 1, 1);
        }
        UnitHpShow.GetComponent<HorizontalLayoutGroup>().gameObject.SetActive(true);
    }
}
