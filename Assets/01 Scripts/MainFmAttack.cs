using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainFmAttack : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;

    
    public GameObject arrowPrefab;  // ȭ�� �������� �־���� ��.
    private float arrowDelay;    // ȭ�� ���� ��Ÿ��

    private RaycastHit2D raycastHit2D;

    List<GameObject> enemys = new List<GameObject>();
    private Vector2 enemyPosition;

    private float timer;
    private bool isAttacking = false;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        arrowDelay = PlayerStatus.Instance.GetPlayerAttackDelay();   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            
 
            raycastHit2D = Physics2D.Raycast(transform.position, enemyPosition, 100, LayerMask.GetMask("Enemy"));

            if (!enemys.Contains(collision.gameObject))
            {
                enemys.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enemys.Contains(collision.gameObject))
        {
            enemys.Remove(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;    // ���� �����̸� ���� Ÿ�̸�

        if (animator.GetBool("IsWalking"))
        {
            isAttacking = false;
        }


        if (timer >= arrowDelay && enemys.Count !=0)
        {
            if (!animator.GetBool("IsWalking"))
            {
                arrowDelay = PlayerStatus.Instance.GetPlayerAttackDelay();
                Shoot();
            }
        }

    }
    private void Shoot()
    {
        timer = 0;
        isAttacking = true;
        animator.SetTrigger("Attack");

        enemyPosition = FindNearestObject(enemys).GetComponent<Rigidbody2D>().position;
        enemyPosition -= rigidbody2D.position;

        if(enemyPosition.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (enemyPosition.x < 0)
        {
            spriteRenderer.flipX = false;
        }
        StartCoroutine(ShootArrow());
 
    }

    IEnumerator ShootArrow()
    {
        WaitForSeconds sec = new WaitForSeconds(arrowDelay / 2.2f);
        animator.SetFloat("AttackSpeed", 1 + (1 - arrowDelay) + (1 - arrowDelay));
        yield return sec;
        if (isAttacking)
        {
            audioSource.Play();
            GameObject arrowObj = Instantiate(arrowPrefab, transform.position, Quaternion.identity);  // ȭ�� ������Ʈ ����.
            ArrowMove arrowMove = arrowObj.GetComponent<ArrowMove>();
            arrowMove.SetTargetDirection(enemyPosition);     // ȭ���� ���� ��ġ�� �̵��� �ƴ϶� �� �������� ���󰡾���.
        }
    }
    private GameObject FindNearestObject(List<GameObject> objects)
    {
        var neareastObject = objects
            .OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.transform.position);
            })
        .FirstOrDefault();

        return neareastObject;
    }
}
