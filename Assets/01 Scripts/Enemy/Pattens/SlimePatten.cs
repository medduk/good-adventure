using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePatten : MonoBehaviour
{
    Enemy slime;
    private new Rigidbody2D rigidbody2D;
    public GameObject Line;
    void Start()
    {
        slime = gameObject.GetComponent<Enemy>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RunPatten());
    }


    IEnumerator RunPatten()
    {
        while (true)
        {
            while (!slime.PlayerCheck)
            {
                yield return null;
            }
            slime.pattening = true;
            rigidbody2D.velocity = Vector2.zero;
            Vector2 T = slime.Player.transform.position - transform.position;
            Debug.Log(T);
            GameObject L = Instantiate(Line, this.transform.position, Quaternion.identity);
            L.GetComponent<PattenLine>().destroytime = 1.0f;
            L.GetComponent<PattenLine>().startshow(T);

            slime.animator.SetTrigger("IsPatten");
            yield return new WaitForSeconds(1.25f);
            rigidbody2D.velocity = T.normalized * slime.EnemyMoveSpeed * 50f * Time.fixedDeltaTime;
            yield return new WaitForSeconds(0.75f);
            slime.animator.SetTrigger("IsPattenEnd");
            slime.pattening = false;

            yield return new WaitForSeconds(10f);

        }
    }
}
