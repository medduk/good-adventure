using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextScript : MonoBehaviour
{
    [SerializeField] int textCount = 10;
    public GameObject damageTextPrefab;
    Queue<GameObject> damageTextQueue = new Queue<GameObject>();

    private void Awake()
    {
        if (textCount < 10) textCount = 10;
    }

    private void InitTextObj(GameObject _damageText)
    {
        _damageText.transform.SetParent(transform);
        _damageText.transform.position = transform.position;
        _damageText.SetActive(false);
        damageTextQueue.Enqueue(_damageText);
    }
    private void SaveQueue()
    {
        if (textCount < 10) textCount = 10;

        for (int i = 0; i < textCount; i++)
        {
            GameObject damageTextObj = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
            InitTextObj(damageTextObj);
        }
    }

    private void Start()
    {
        SaveQueue();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime);
    }

    public void DisplayDamage(int _damage)
    {
    }
}
