using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private GameObject enemyManager;
    [SerializeField] GameObject dustukPrefab;   // ø©±‚ «¡∏Æ∆’ ≥÷æÓ¡‹.
    public Transform[] enemyTransforms;

    private void Awake()
    {
        enemyManager = GameObject.Find("EnemyManager");

        enemyTransforms = new Transform[transform.childCount+1];

        int i = 0;
        foreach (Transform et in transform.GetComponentsInChildren<Transform>())
        {
            if(et.name.Contains("EnemyPosition"))
            {
                enemyTransforms[i] = et;
                i++;
            }
        }
    }

    private void Start()
    {
        if (transform.childCount > 1)
        {
            for (int i = 1; i <= transform.childCount; i++)
            {
                GameObject enemy = Instantiate(dustukPrefab, enemyTransforms[i].position, Quaternion.identity);
                enemy.transform.SetParent(enemyManager.transform);
            }
        }
    }
}
