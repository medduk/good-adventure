using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private Transform enemyManager;
    List<Transform> enemys;

    private Vector2 targetDir2D;
    public float arrowSpeed = 4f;

    int richochet;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        enemyManager = GameObject.Find("EnemyManager").transform;
    }

    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(PlayerStatus.Instance.CalPlayerDamage());

            if (richochet > 0)
            {
                for (int i = 0; i < enemyManager.childCount; i++)
                {
                    if (enemyManager.GetChild(i) != collision.transform)
                    {
                        enemys.Add(enemyManager.GetChild(i).Find("EnemyDetecter"));
                    }
                }

                if (enemys.Count > 0)
                {
                    Transform nearEnemy = FindNearestObject(enemys);
                    if (Vector3.Distance(transform.position, nearEnemy.position) < 10f)
                    {
                        richochet--;

                        targetDir2D = nearEnemy.position;

                        float angle = (Mathf.Atan2(targetDir2D.y, targetDir2D.x) * Mathf.Rad2Deg);
                        Quaternion angleAxis = Quaternion.AngleAxis(angle - 180, Vector3.forward);
                        transform.rotation = angleAxis;

                        return;
                    }
                }
                
            }
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            gameObject.SetActive(false);
        }
    }

    public void StartArrow(Vector2 enemyPosition)
    {
        enemys = new List<Transform>();

        richochet = PlayerStatus.Instance.playerSkills[(int)PlayerStatus.ShotSkills.ricochetShot];

        targetDir2D = enemyPosition;

        float angle = (Mathf.Atan2(targetDir2D.y, targetDir2D.x) * Mathf.Rad2Deg);
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 180, Vector3.forward);
        transform.rotation = angleAxis;
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = targetDir2D.normalized * arrowSpeed * 30f * Time.fixedDeltaTime;
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
}
