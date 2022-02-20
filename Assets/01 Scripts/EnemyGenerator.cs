using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private GameObject enemyManager;
    [SerializeField] GameObject dustukPrefab;   // 여기 프리팹 넣어줌.
    private Transform[] enemyTransforms;

    private void Awake()
    {
        enemyManager = GameObject.Find("EnemyManager");

        enemyTransforms = GetComponentsInChildren<Transform>(); // 이거 자기자신까지 가져옴 그래서 0이아니라 1부터 시작해야할듯.
        if (transform.childCount > 1)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                GameObject.Instantiate(dustukPrefab, enemyTransforms[i].position,Quaternion.identity).transform.parent = enemyManager.transform;
            }
        }
    }
}
