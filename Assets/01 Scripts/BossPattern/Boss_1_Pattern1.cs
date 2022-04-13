using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1_Pattern1 : MonoBehaviour
{
    float time;
    List<GameObject> Thing = new List<GameObject>();
    [SerializeField] GameObject Image;


    [SerializeField] float patternForce = 12.5f;

    void Update()
    {
        time += Time.deltaTime;

        if (time > 2f)
        {
            if (Image.activeSelf)
            {
                Image.SetActive(false);
            }
            for (int i = 0; i < Thing.Count; i++)
            {
                if (Thing[i].layer == LayerMask.NameToLayer("Enemy"))
                {
                    if (Thing[i].GetComponent<Enemy>().PlayerCheck)
                        Thing[i].GetComponent<Enemy>().PlayerCheck = false;
                }
                Thing[i].GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(Thing[i].transform.position, transform.position, Time.deltaTime * patternForce);

            }
        }   
        if (time > 7f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Thing.Add(collision.gameObject);
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            if(collision.gameObject.transform.tag != "Boss")
            {
                Thing.Add(collision.gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (Thing.Contains(collision.gameObject))
        {
            Thing.Remove(collision.gameObject);
        }
    }
}
