using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MainFmAttack : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    #region Test
    private LineRenderer lineRenderer;
    #endregion

    public GameObject arrowPrefab;  // ȭ�� �������� �־���� ��.
    private float arrowDelay;    // ȭ�� ���� ��Ÿ��

    private RaycastHit2D raycastHit2D;

    // �� ����
    //Dictionary<Collider2D, float> enemys = new Dictionary<Collider2D, float>();
    List<GameObject> enemys = new List<GameObject>();
    private Vector2 enemyPosition;

    private float timer;

    private bool isAttacking = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        arrowDelay = PlayerStatus.Instance.GetPlayerAttackDelay();
    }

    /* ���� ������ ���� �ٸ� ������ ���� �Ұ��ΰ�? */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            raycastHit2D = Physics2D.Raycast(transform.position, collision.GetComponent<Rigidbody2D>().position, 100, LayerMask.GetMask("Enemy"));

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

    private void Update()
    {
        timer += Time.deltaTime;    // ���� �����̸� ���� Ÿ�̸�

        if (animator.GetBool("isWalking"))
        {
            isAttacking = false;
        }

        if (timer >= arrowDelay && enemys.Count !=0)
        {
            if (!animator.GetBool("isWalking"))
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

        if (enemyPosition.x > 0)        // ���� ĳ������ ��ġ�� ���� ���� ��ȯ ����.
        {
            spriteRenderer.flipX = true;
        }
        else if(enemyPosition.x < 0)
        {
            spriteRenderer.flipX = false;
        }

        StartCoroutine(ShootArrow());
    }

    IEnumerator ShootArrow()
    {
        WaitForSeconds sec = new WaitForSeconds(arrowDelay / 2.2f) ;
        animator.SetFloat("AttackSpeed", 1 + (1 - arrowDelay) + (1-arrowDelay));
        yield return sec;
        if (isAttacking)
        {
            GameObject arrowObj = Instantiate(arrowPrefab, transform.position, Quaternion.identity);  // ȭ�� ������Ʈ ����.
            ArrowMove arrowMove = arrowObj.GetComponent<ArrowMove>();
            arrowMove.SetTargetDirection(enemyPosition);     // ȭ���� ���� ��ġ�� �̵��� �ƴ϶� �� �������� ���󰡾���.
        }
    }

    private GameObject FindNearestObject(List<GameObject> objects)
    {
        // LINQ �޼ҵ带 �̿��� ���� ����� ���� ã���ϴ�.
        var neareastObject = objects
            .OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.transform.position);
            })
        .FirstOrDefault();

        return neareastObject;
    }
}
