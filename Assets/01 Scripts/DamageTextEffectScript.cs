using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class DamageTextEffectScript : MonoBehaviour
{
    private TextMeshPro damageText;
    public Color damageTextColor;
    
    [SerializeField] float textMoveSpped = 1f;
    [SerializeField] float alphaSpeed = 1f;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
    }
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * textMoveSpped);
        damageTextColor.a = Mathf.Lerp(damageTextColor.a, 0, Time.deltaTime * alphaSpeed);
        damageText.color = damageTextColor;
    }
}
