using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCircle : MonoBehaviour
{
    #region Move
    [SerializeField] float rotateSpeed = 1f;
    #endregion

    #region Attack
    [SerializeField] float damagePercent = 0.3f;
    [SerializeField] float circleAttackDelay = 0.5f;
    #endregion

    private static List<Collider2D> attackCheckList = new List<Collider2D>();

    void Update()
    {
        transform.RotateAround(transform.parent.position, Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if (!attackCheckList.Contains(collision))
            {
                attackCheckList.Add(collision);
                collision.GetComponent<Enemy>().TakeDamage(((int)(PlayerStatus.Instance.CalPlayerDamage().Item1 * damagePercent), PlayerStatus.Instance.CalPlayerDamage().Item2));
                StartCoroutine(CheckOutCollider(collision));
            }
        }
    }

    IEnumerator CheckOutCollider(Collider2D collider)
    {
        yield return new WaitForSeconds(circleAttackDelay);
        if (attackCheckList.Contains(collider))
        {
            attackCheckList.Remove(collider);
        }
    }
}
