using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 튜토리얼이 끝났을 때 알려줍니다. */
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
