using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private GameObject enemyManager;
    [SerializeField] GameObject[] enemyPrefabs;  // 맵에 등장 가능한 몬스터 종류
    [SerializeField] int[] monsterChance;  // 몬스터에 따라 등장확률
    private int sum = 0;
    public Transform[] enemyTransforms; // 몬스터 생성위치

    private void Awake()
    {
        enemyManager = GameObject.Find("EnemyManager");

        if(monsterChance.Length < enemyPrefabs.Length) // 에너미 프리팹 배열 개수에 비해 확률 배열의 개수가 적을 경우 같은 크기만큼 배열을 늘리고 늘린 배열은 1로 초기화.
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
        }

        enemyTransforms = new Transform[transform.childCount];

        int i = 0;
        foreach (Transform et in transform.GetComponentsInChildren<Transform>())  // 맵의 정보를 읽어 몬스터의 생성위치를 저장한다
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
                enemy.transform.name = "Dustuk" + i;
            }
        }
    }

    private int SelectRandomEnemyPrefab()  // 몬스터 랜덤뽑기
    {
        int randomIndex = Random.Range(1, sum+1);

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
        return i;
    }
}