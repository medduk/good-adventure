using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    private Vector2 targetDir2D;

    public float arrowSpeed = 5f;       // 화살 날라가는 속도
    public float rot = 5f;

    private int arrowDamage;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        arrowDamage = PlayerStatus.Instance.GetPlayerDamage();

        StartCoroutine(remove());

        float angle = (Mathf.Atan2(targetDir2D.y, targetDir2D.x) * Mathf.Rad2Deg) - 90;
        Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        transform.rotation = angleAxis;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(arrowDamage);
            Destroy(gameObject);
        }

        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = targetDir2D.normalized * arrowSpeed * 30f * Time.fixedDeltaTime;
    }
    IEnumerator remove()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void SetTargetDirection(Vector2 enemyPosition)
    {
        targetDir2D = enemyPosition;
    }
}
