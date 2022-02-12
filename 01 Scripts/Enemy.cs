using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidbody2D;

    [SerializeField] Slider enemyHpSlider;

    [SerializeField] int enemyMaxHp = 100;  // 시작 체력.
    [SerializeField] int enemyHp;
    [SerializeField] float enemyMoveSpeed = 5f;
    [SerializeField] int enemyDamage;

    private bool playerCheck = false;
    private GameObject player;
    private Vector3 playerDir;

    private bool stopMove = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemyHp = enemyMaxHp;       // 시작 할때 최대 체력.
        enemyHpSlider.value = enemyHp/enemyMaxHp;    // 풀피 시작.
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerCheck = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
            rigidbody2D.velocity =  playerDir.normalized * enemyMoveSpeed * Time.fixedDeltaTime;
        }
        
    }

    public void TakeDamage(int damage)
    {
        playerCheck = true;
        enemyHp -= damage;
        enemyHpSlider.value = (float)enemyHp / enemyMaxHp;

        if (enemyHp <=0) DieEnemy();
        else
        {
            animator.SetTrigger("isHit");
            StartCoroutine(StopMove());
        }
    }

    private void DieEnemy() {
        Destroy(gameObject);
    }

    public IEnumerator StopMove()
    {
        stopMove = true;
        rigidbody2D.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.1f);
        stopMove = false;
    }
}
