using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ʃ�丮���� ������ �� �˷��ݴϴ�. */
public class TutorialStage : MonoBehaviour
{
    [Header("For Attack Tutorial")]
    public bool isAttackTutorial = false;

    [Header("For End Tutorial")]
    public GameObject portal;
    public bool isEndTutorial = false;


    private void Start()
    {
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
            if (isAttackTutorial)   // ���� Ʃ�丮���̶��
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
        if (!ContinueDataManager.skip)  // �ɼǿ��� skip�� �ƴ϶��
        {
            DialogManager.Instance.Action(gameObject);
        }
        Destroy(gameObject);
    }
}
