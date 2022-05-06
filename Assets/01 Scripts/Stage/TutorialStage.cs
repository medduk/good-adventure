using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Ʃ�丮���� ������ �� �˷��ݴϴ�. */
public class TutorialStage : MonoBehaviour
{
    public GameObject portal;

    private void Start()
    {
        StartCoroutine(ActiveEndOfTutorial());
    }

    IEnumerator ActiveEndOfTutorial()
    {
        yield return new WaitUntil(() => portal.activeSelf && StageManager.Instance.isClear);
        DialogManager.Instance.Action(gameObject);
    }
}
