using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    public GameObject enemyManager;
    [SerializeField] GameObject dustukPrefab;   // ���� ������ �־���.
    private Transform[] enemyTransforms;

    private void Awake()
    {
        enemyManager = GameObject.Find("EnemyManager");

        enemyTransforms = GetComponentsInChildren<Transform>(); // �̰� �ڱ��ڽű��� ������ �׷��� 0�̾ƴ϶� 1���� �����ؾ��ҵ�.
        if (transform.childCount > 1)
        {
            for (int i = 1; i < transform.childCount; i++)
            {
                GameObject.Instantiate(dustukPrefab, enemyTransforms[i]).transform.parent = enemyManager.transform;
                Debug.Log(enemyManager.transform.name);
            }
        }
    }
}
