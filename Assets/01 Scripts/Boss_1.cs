using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    private GameObject P1;
    public GameObject[] enemy;
    public GameObject[] Patterns;

    BossEnemy bossEnemy;

    private bool playPattern1 = false;
    private float time;

    [SerializeField] float warningTime = 2f;
    [SerializeField] float patternMaintainTime = 3f;

    void Start()
    {
        bossEnemy = gameObject.GetComponent<BossEnemy>();
        bossEnemy.hitPattern += Hit;
        bossEnemy.diePattern += DIE;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if(time > 20)
        {
            time = 0f;
            StartCoroutine(Pattern1());
        }
    }
        private void OnCollisionStay2D(Collision2D collision)
    {
        if (playPattern1)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (collision.gameObject.GetComponent<Enemy>().IDName == "Dustuk")
                {
                    collision.gameObject.GetComponent<Enemy>().TriggerDie();
                    bossEnemy.EnemyHp += 30;

                    if (bossEnemy.EnemyHp > bossEnemy.EnemyMaxHp)
                    {
                        bossEnemy.EnemyHp = bossEnemy.EnemyMaxHp;
                    }
                    bossEnemy.Hpbar();
                    sizeChange();
                }

            }
        }

    }
    void Hit()
    {
        sizeChange();
        GameObject a = Instantiate(enemy[1],transform.position, Quaternion.identity);
        a.GetComponent<Enemy>().EnemyGiveExp = 0;
        a.GetComponent<Enemy>().EnemyMaxHp = 20;
        a.GetComponent<Enemy>().cangiveItem = false;
        a.transform.parent = GameObject.Find("EnemyManager").transform;
    }

    void DIE()
    {
        Destroy(P1);
    }

    IEnumerator Pattern1()
    {
        bossEnemy.animator.SetBool("pattern1", true);
        P1 = Instantiate(Patterns[0], transform.position, Quaternion.identity);
        yield return new WaitForSeconds(warningTime);
        playPattern1 = true;
        yield return new WaitForSeconds(patternMaintainTime);
        Destroy(P1);
        playPattern1 = false;
        bossEnemy.animator.SetBool("pattern1", false);
        GameObject b = Instantiate(enemy[0], new Vector3(transform.position.x, transform.position.y + 1.75f), Quaternion.identity);
        b.GetComponent<Enemy>().EnemyGiveExp = 0;
        b.GetComponent<Enemy>().cangiveItem = false;
        b.transform.parent = GameObject.Find("EnemyManager").transform;
    }
    void sizeChange()
    {
        float S = (float)(bossEnemy.EnemyMaxHp - bossEnemy.EnemyHp) / (float)(bossEnemy.EnemyMaxHp * 0.5);
        gameObject.transform.localScale = new Vector3(3f - S, 3f - S);
    }
}
