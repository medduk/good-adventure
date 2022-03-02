using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private Vector2 targetDir2D;
    public float arrowSpeed = 4f;
    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(PlayerStatus.Instance.CalPlayerDamage());
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            gameObject.SetActive(false);
        }
    }

    public void StartArrow(Vector2 enemyPosition)
    {
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
