using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PattenLine : MonoBehaviour
{
    public float destroytime = 1f;
    private new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            rigidbody2D.velocity = Vector3.zero;
            Destroy(gameObject,destroytime);
        }
    }

    public void startshow(Vector3 end)
    {

        rigidbody2D.velocity = end.normalized * 750f * Time.fixedDeltaTime;
    }
    // Update is called once per frame

}
