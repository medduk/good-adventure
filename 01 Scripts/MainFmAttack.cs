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

    public GameObject arrowPrefab;  // 화살 프리팹을 넣어줘야 함.
    private float arrowDelay;    // 화살 공격 쿨타임

    private RaycastHit2D raycastHit2D;

    // 적 관련
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

    /* 공격 범위가 서로 다른 공격이 존재 할것인가? */
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
        timer += Time.deltaTime;    // 공격 딜레이를 위한 타이머

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

        if (enemyPosition.x > 0)        // 적과 캐릭터의 위치에 따라 방향 전환 설정.
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
            GameObject arrowObj = Instantiate(arrowPrefab, transform.position, Quaternion.identity);  // 화살 오브젝트 생성.
            ArrowMove arrowMove = arrowObj.GetComponent<ArrowMove>();
            arrowMove.SetTargetDirection(enemyPosition);     // 화살은 적의 위치로 이동이 아니라 적 방향으로 날라가야함.
        }
    }

    private GameObject FindNearestObject(List<GameObject> objects)
    {
        // LINQ 메소드를 이용해 가장 가까운 적을 찾습니다.
        var neareastObject = objects
            .OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.transform.position);
            })
        .FirstOrDefault();

        return neareastObject;
    }
}
