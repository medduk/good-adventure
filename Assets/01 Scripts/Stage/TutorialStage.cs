using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 튜토리얼이 끝났을 때 알려줍니다. */
public class TutorialStage : MonoBehaviour
{
    [Header("For Attack Tutorial")]
    public bool isAttackTutorial = false;

    [Header("For End Tutorial")]
    public GameObject portal;
    public bool isEndTutorial = false;

    private void Start()
    {
        if (ContinueDataManager.skip)
        {
            Destroy(gameObject);
        }

        if (isEndTutorial)
        {
            StartCoroutine(ActiveEndOfTutorial());
        }
    }

    IEnumerator ActiveEndOfTutorial()
    {
        yield return new WaitUntil(() => portal.activeSelf && StageManager.Instance.isClear);
        DialogManager.Instance.Action(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (isAttackTutorial)   // 공격 튜토리얼이라면
            {
                StartCoroutine(ReadyAttack());
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                DialogManager.Instance.Action(gameObject);
                Destroy(gameObject);
            }
        }
    }

    IEnumerator ReadyAttack()
    {
        yield return new WaitUntil(() => StageManager.Instance.enemyManager.transform.childCount <= 1);
        if (!ContinueDataManager.skip)  // 옵션에서 skip이 아니라면
        {
            DialogManager.Instance.Action(gameObject);
        }
        Destroy(gameObject);
    }
}
