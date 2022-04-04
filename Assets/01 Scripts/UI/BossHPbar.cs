using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPbar : MonoBehaviour
{
    [SerializeField] Slider BossHP;
    [SerializeField] Slider BossHPEffect;

    public Slider GetBossHP
    {
        get
        {
            return BossHP;
        }
    }
    public Slider GetBossHPEffect
    {
        get
        {
            return BossHPEffect;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
