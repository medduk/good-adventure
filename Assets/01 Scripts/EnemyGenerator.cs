using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private GameObject enemyManager;
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] int[] monsterChance;
    private int sum = 0;
    public Transform[] enemyTransforms;

    private void Awake()
    {
        enemyManager = GameObject.Find("EnemyManager");

        if(monsterChance.Length < enemyPrefabs.Length)
        {
            int[] temp = new int[enemyPrefabs.Length];
            System.Array.Copy(monsterChance, temp, monsterChance.Length);

            for(int c=0; c<enemyPrefabs.Length - monsterChance.Length; c++)
            {
                temp[c] = 1;
            }

            monsterChance = temp.Clone() as int[];
        }

        for(int c = 0; c< enemyPrefabs.Length; c++)
        {
            sum += monsterChance[c];
            Debug.Log("SumSum:" + sum);
        }

        enemyTransforms = new Transform[transform.childCount];

        int i = 0;
        foreach (Transform et in transform.GetComponentsInChildren<Transform>())
        {
            if (et.name.Contains("EnemyPosition"))
            {
                enemyTransforms[i] = et;
                et.gameObject.SetActive(false);
                i++;
            }
        }
    }

    private void Start()
    {

        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                int enemyIndex = SelectRandomEnemyPrefab();
                GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], enemyTransforms[i].position, Quaternion.identity);
                enemy.transform.SetParent(enemyManager.transform);
            }
        }
    }

    private int SelectRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(1, sum+1);
        Debug.Log(randomIndex);

        int i = 0;
        while (i < enemyPrefabs.Length)
        {
            randomIndex = randomIndex - monsterChance[i];
            if (randomIndex<= 0)
            {
                break;
            }
            i++;
        }
        Debug.Log(i);
        return i;
    }
}