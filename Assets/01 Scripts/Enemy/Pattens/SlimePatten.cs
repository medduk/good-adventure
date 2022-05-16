using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatten : MonoBehaviour
{
    Enemy slime;
    private new Rigidbody2D rigidbody2D;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        slime = gameObject.GetComponent<Enemy>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RunPatten());
    }


    IEnumerator RunPatten()
    {
        while (!slime.PlayerCheck)
        {
            yield return null;
        }
        Vector3 T = player.transform.position;
        slime.pattening = true;
        slime.animator.SetTrigger("IsPatten");
        yield return new WaitForSeconds(1.25f);
        rigidbody2D.velocity = T * slime.EnemyMoveSpeed * 30f * Time.fixedDeltaTime;
        yield return new WaitForSeconds(0.75f);
        slime.animator.SetTrigger("IsPattenEnd");
        slime.pattening = false;

    }
}
