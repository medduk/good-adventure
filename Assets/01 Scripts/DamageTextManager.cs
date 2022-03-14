using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField] [Range(10, 30)] int textPoolingCount = 10;
    public float textMaintainTime = 2f;

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

        if (textPoolingCount < 10) textPoolingCount = 10;
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
        SaveQueue(textPoolingCount);
    }

    public void DisplayDamage(int _damage,Vector3 _position,bool isCritical = false)
    {
        if (damageTextQueue.Count <= textPoolingCount/2)
        {
            SaveQueue(textPoolingCount/2);
        }

        TextMeshPro damageText = damageTextQueue.Dequeue().GetComponent<TextMeshPro>();
        DamageTextEffectScript dtes = damageText.GetComponent<DamageTextEffectScript>();
        damageText.transform.SetParent(null);
        damageText.transform.position = _position + new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(0,0.3f), 0);
        damageText.text = _damage + "";
        dtes.damageTextColor = isCritical ? new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 1) : new Color(Color.white.r, Color.white.g, Color.white.b, 1);
        damageText.gameObject.SetActive(true);
        dtes.MoveText();
        StartCoroutine(ReturnTextObj(damageText.gameObject));
    }

    IEnumerator ReturnTextObj(GameObject _damageText)
    {
        WaitForSeconds sec = new WaitForSeconds(textMaintainTime);
        yield return sec;
        InitTextObj(_damageText);
    }
}
