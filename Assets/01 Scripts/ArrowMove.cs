using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private Transform enemyManager;

    private Vector2 targetDir2D;
    public float arrowSpeed = 4f;

    List<Transform> enemys;
    List<Transform> hitEnemys;
    private int ricochetCount = 0;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        enemyManager = GameObject.Find("EnemyManager").transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(PlayerStatus.Instance.CalPlayerDamage());

            hitEnemys.Add(collision.transform);

            if (ricochetCount <= 0)
            {
                gameObject.SetActive(false);
            }

            else
            {
                ricochetCount--;
                enemys.Clear();

                for (int i = 0; i < enemyManager.childCount; i++)
                {
                    Transform temp = enemyManager.GetChild(i);

                    if (Vector3.Distance(transform.position, temp.position) < 3f && temp != collision.transform && !hitEnemys.Contains(temp))
                    {
                        enemys.Add(temp);
                    }
                }

                if (enemys.Count > 0) {

                    targetDir2D = FindNearestObject(enemys).position - transform.position;

                    float angle = (Mathf.Atan2(targetDir2D.y, targetDir2D.x) * Mathf.Rad2Deg);
                    Quaternion angleAxis = Quaternion.AngleAxis(angle - 180, Vector3.forward);
                    transform.rotation = angleAxis;
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            gameObject.SetActive(false);
        }
    }

    private Transform FindNearestObject(List<Transform> objects)
    {
        var neareastObject = objects
            .OrderBy(obj =>
            {
                return Vector3.Distance(transform.position, obj.position);
            })
        .FirstOrDefault();

        return neareastObject;
    }

    public void StartArrow(Vector2 enemyPosition)
    {
        enemys = new List<Transform>();
        hitEnemys = new List<Transform>();
        ricochetCount = PlayerStatus.Instance.playerSkills[(int)PlayerStatus.ShotSkills.ricochetShot];

        targetDir2D = enemyPosition;

        float angle = (Mathf.Atan2(targetDir2D.y, targetDir2D.x) * Mathf.Rad2Deg);
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        transform.rotation = angleAxis;

    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = targetDir2D.normalized * arrowSpeed * 30f * Time.fixedDeltaTime;
    }



}
