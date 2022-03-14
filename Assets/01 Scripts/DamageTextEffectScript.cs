using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class DamageTextEffectScript : MonoBehaviour
{
    private TextMeshPro damageText;
    public Color damageTextColor;
    
    [SerializeField] float textMoveSpeed = 1f;
    [SerializeField] float alphaSpeed = 1f;

    Vector2 randomDir;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>();
    }

    // 먼저 1/2만큼 올라갔다가. 그 이후에는 내려가야함.

    void Update()
    {
        transform.Translate(randomDir * textMoveSpeed * Time.deltaTime);

        damageTextColor.a = Mathf.Lerp(damageTextColor.a, 0, Time.deltaTime * alphaSpeed);
        damageText.color = damageTextColor;
    }

    public void MoveText()
    {
        randomDir = new Vector2(Random.Range(-1f, 1f)*2f, 1);
        StartCoroutine(ChangeMove(DamageTextManager.Instance.textMaintainTime));
    }

    IEnumerator ChangeMove(float _time)
    {
        yield return new WaitForSeconds(_time / 2);
        randomDir = new Vector2(randomDir.x*0.3f,-1);
    }
}
