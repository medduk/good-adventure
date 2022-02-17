using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;

    [SerializeField] Slider enemyHpSlider;
    private SpriteRenderer spriteRenderer;

    private CircleCollider2D circleCollider2D;

    [SerializeField] int enemyMaxHp = 100;  // 시작 체력.
    [SerializeField] int enemyHp;
    [SerializeField] float enemyMoveSpeed = 5f;
    [SerializeField] int enemyDamage;

    List<Enemy> enemyFriends = new List<Enemy>();    // 일정 범위 이내 친구가 맞았을 경우 같이 쫓아가줌.
    private bool playerCheck = false;
    private GameObject player;
    private Vector3 playerDir;

    private bool stopMove = false;

    static WaitForSeconds sec;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        /* 죽고 나서 꺼주기 위한 용도*/
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        enemyHp = enemyMaxHp;       // 시작 할때 최대 체력.
        enemyHpSlider.value = 1f;    // 풀피 시작.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerCheck = true;
            animator.SetBool("isWalking",true);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyFriends.Add(collision.gameObject.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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
        }
    }
    
    private void FixedUpdate()
    {
        if(playerCheck && !stopMove)
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

            rigidbody2D.velocity =  playerDir.normalized * enemyMoveSpeed * Time.fixedDeltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        playerCheck = true;
        animator.SetBool("isWalking", true);

        for (int i = 0; i < enemyFriends.Count; i++)
        {
            enemyFriends[i].FriendHit();
        }
        enemyHp -= damage;
        enemyHpSlider.value = (float)enemyHp / enemyMaxHp;

        if (enemyHp <=0) StartCoroutine(DieEnemy());
        else
        {
            StartCoroutine(StopMove());
        }
    }

    /* 죽는 모션이 다 나오도록 하기 위한 코루틴 */
    private IEnumerator DieEnemy() {

        animator.SetTrigger("Die");
        
        /* 죽었으니까 게임에 영항을 미칠만한 컴포넌트들을 다 꺼준다. */
        circleCollider2D.enabled = false;
        enemyHpSlider.gameObject.SetActive(false);

        /* 죽었을 때 움직이는 것 방지. */
        stopMove = true;
        rigidbody2D.velocity = Vector2.zero;

        sec = new WaitForSeconds(0.3f);
        yield return sec;

        Destroy(gameObject);
    }

    public IEnumerator StopMove()
    {
        animator.SetTrigger("isHit");

        stopMove = true;
        rigidbody2D.velocity = Vector2.zero;

        sec = new WaitForSeconds(0.5f);
        yield return sec;
        stopMove = false;
    }
}
