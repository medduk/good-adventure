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
    public Transform quiverObject;
    Queue<GameObject> quiver;

    [Tooltip("Warning! It's needed at leat more than 10")]
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
        SaveQueue(arrowPoolingCount);
    }
    private void SaveQueue(int arrowsCount)
    {
        if (arrowsCount < 10) arrowsCount = 10;

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

        if (timer >= PlayerStatus.Instance.PlayerAttackDelay && enemys.Count != 0)
        {
            if (!animator.GetBool("IsWalking"))
            {
                PlayerStatus.Instance.PlayerAttackDelay = PlayerStatus.Instance.PlayerAttackDelay;
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
        WaitForSeconds sec = new WaitForSeconds(PlayerStatus.Instance.PlayerAttackDelay * 0.55f);
        animator.SetFloat("AttackSpeed", 1 + (1 - PlayerStatus.Instance.PlayerAttackDelay) + (1 - PlayerStatus.Instance.PlayerAttackDelay));
        int chainNum = PlayerStatus.Instance.playerSkills[(int)PlayerStatus.ShotSkills.chainShot];
        yield return sec;
        while (isAttacking && chainNum + 1 > 0)
        {
            chainNum--;
            audioSource.Play();
            if (quiver.Count > 0)
            {
                int multi = 0;
                if ((multi = PlayerStatus.Instance.playerSkills[(int)PlayerStatus.ShotSkills.multiShot]) > 0)
                {
                    multi++;
                    GameObject[] arrows = new GameObject[multi];
                    for (int i = 0; i < multi; i++)
                    {
                        arrows[i] = quiver.Dequeue();
                        if (i % 2 == 1)
                        {
                            arrows[i].transform.position = new Vector3(quiverObject.position.x - 0.16f * (i / 2 + 1) * enemyPosition.normalized.y
                                , quiverObject.position.y + 0.16f * (i / 2 + 1) * enemyPosition.normalized.x, quiverObject.position.z);
                        }
                        else
                        {
                            arrows[i].transform.position = new Vector3(quiverObject.position.x + 0.16f * (i / 2 + 1) * enemyPosition.normalized.y
                                , quiverObject.position.y - 0.16f * (i / 2 + 1) * enemyPosition.normalized.x, quiverObject.position.z);
                        }
                    }

                    for (int i = 0; i < multi; i++)
                    {
                        arrows[i].transform.SetParent(null);
                        arrows[i].GetComponent<ArrowMove>().StartArrow(enemyPosition);
                        arrows[i].SetActive(true);
                        StartCoroutine(ReturnArrow(arrows[i]));
                    }
                }
                else
                {
                    GameObject arrow = quiver.Dequeue();
                    arrow.transform.SetParent(null);
                    arrow.GetComponent<ArrowMove>().StartArrow(enemyPosition);
                    arrow.SetActive(true);
                    StartCoroutine(ReturnArrow(arrow));
                }
            }
            else
            {
                for (int i = 0; i < PlayerStatus.Instance.playerSkills[(int)PlayerStatus.ShotSkills.multiShot] * 2 + 1 ; i++)
                {
                    GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                    InitArrow(arrow);
                }
            }
            sec = new WaitForSeconds(PlayerStatus.Instance.PlayerAttackDelay * 0.3f);
            yield return sec;
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