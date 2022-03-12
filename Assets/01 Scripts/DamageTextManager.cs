using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField] int textCount = 10;
    [SerializeField] float textMaintainTime = 2f;

    public GameObject damageTextPrefab;
    
    Queue<GameObject> damageTextQueue = new Queue<GameObject>();

    private static DamageTextManager instance = null;

    public static DamageTextManager Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            return null;
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        if (textCount < 10) textCount = 10;
    }

    private void InitTextObj(GameObject _damageText)
    {
        _damageText.transform.SetParent(transform);
        _damageText.transform.position = transform.position;
        _damageText.SetActive(false);
        damageTextQueue.Enqueue(_damageText);
    }
    private void SaveQueue(int _textCount)
    {
        for (int i = 0; i < _textCount; i++)
        {
            GameObject damageTextObj = Instantiate(damageTextPrefab, transform.position, Quaternion.identity);
            InitTextObj(damageTextObj);
        }
    }

    private void Start()
    {
        SaveQueue(textCount);
    }

    public void DisplayDamage(int _damage,Vector3 _position)
    {
        if (damageTextQueue.Count <= textCount/2)
        {
            SaveQueue(textCount/2);
        }
        TextMeshPro damageText = damageTextQueue.Dequeue().GetComponent<TextMeshPro>();
        damageText.transform.SetParent(null);
        damageText.transform.position = _position;
        damageText.text = _damage + "";
        damageText.gameObject.SetActive(true);
        StartCoroutine(ReturnTextObj(damageText.gameObject));
    }

    IEnumerator ReturnTextObj(GameObject _damageText)
    {
        WaitForSeconds sec = new WaitForSeconds(textMaintainTime);
        yield return sec;
        InitTextObj(_damageText);
    }
}
