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

    public GameObject arrowPrefab;  // 화살 프리팹을 넣어줘야 함.
    private float arrowDelay;    // 화살 공격 쿨타임
    public Transform quiverObject;
    Queue<GameObject> quiver;
    [Tooltip("Warning! It's needed at leat more than 6")]
    public int arrowPoolingCount;

    private RaycastHit2D raycastHit2D;

    List<GameObject> enemys = new List<GameObject>();
    private Vector2 enemyPosition;

    private float timer;
    private bool isAttacking = false;

    public float arrowAliveTime;

    private bool isGameOver = false;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        quiver = new Queue<GameObject>();

        isGameOver = false;
    }

    private void Start()
    {
        arrowDelay = PlayerStatus.Instance.GetPlayerAttackDelay();

        SaveQueue(arrowPoolingCount);
    }
    private void SaveQueue(int arrowsCount)
    {
        for (int i = 0; i < arrowsCount; i++)
        {
            GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
            InitArrow(arrow);
        }
    }
    private void InitArrow(GameObject arrow)
    {
        arrow.transform.SetParent(quiverObject);
        arrow.transform.position = quiverObject.position;
        arrow.SetActive(false);
        quiver.Enqueue(arrow);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
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
        if(isGameOver)
        {
            return;
        }
        timer += Time.deltaTime;    // 공격 딜레이를 위한 타이머

        if (animator.GetBool("IsWalking"))
        {
            isAttacking = false;
        }

        if (timer >= arrowDelay && enemys.Count != 0)
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

        if (enemyPosition.x > 0)
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
            if (quiver.Count > 0)
            {
                GameObject arrow = quiver.Dequeue();
                arrow.transform.SetParent(null);
                arrow.GetComponent<ArrowMove>().StartArrow(enemyPosition);
                arrow.SetActive(true);
                StartCoroutine(ReturnArrow(arrow));
            }
            else
            {
                GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                InitArrow(arrow);
            }
        }
    }

    IEnumerator ReturnArrow(GameObject arrow)
    {
        WaitForSeconds sec = new WaitForSeconds(arrowAliveTime);
        yield return sec;
        InitArrow(arrow);
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

    public void SetGameOver()
    {
        isGameOver = true;
    }
}