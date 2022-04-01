using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    public GameObject[] enemy;
    BossEnemy bossEnemy;
    // Start is called before the first frame update
    void Start()
    {
        bossEnemy = gameObject.GetComponent<BossEnemy>();
        bossEnemy.hitPattern += Hit;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Hit()
    {
        float S = (float)(bossEnemy.EnemyMaxHp - bossEnemy.EnemyHp) / (float)(bossEnemy.EnemyMaxHp*0.5);
        gameObject.transform.localScale = new Vector3(3f-S, 3f-S);
        GameObject a = Instantiate(enemy[1],transform.position, Quaternion.identity);
        a.GetComponent<Enemy>().EnemyGiveExp = 0;
        a.GetComponent<Enemy>().EnemyMaxHp = 20;
        a.GetComponent<Enemy>().cangiveItem = false;
        a.transform.parent = GameObject.Find("EnemyManager").transform;
    }
}
