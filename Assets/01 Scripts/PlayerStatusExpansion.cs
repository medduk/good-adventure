using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusExpansion : MonoBehaviour
{
    [SerializeField] GameObject sayBox; // 현재 대화중인가

    private DialogManager dialogManager;

    private void Start()
    {
        dialogManager = DialogManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)  // 각종 NPC 및 사물 상호작용, 본 게임에서는 원터치 진행을 선택하여 사용자의 편의성을 위해 접촉으로 모든 상호작용을 시도하게 하였음
    {
        if (collision.transform.tag == "Portal")
        {
            StageManager.Instance.MoveNextStage();
        }

        if (collision.transform.tag == "ReinForce")
        {
            Destroy(collision.gameObject);
            GameManager.Instance.OpenReinForce();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("NPC"))
        {
            if (collision.transform.tag == "Rune")
            {
                StartCoroutine(RuneNPCmeet(collision.gameObject));
            }
            if (collision.transform.tag == "BlackSmith")
            {
                StartCoroutine(BlackSmithNPCmeet(collision.gameObject));
            }
            if (collision.transform.tag == "SHOP")
            {
                StartCoroutine(SHOPNPCmeet(collision.gameObject));
            }
        }
    }

    IEnumerator RuneNPCmeet(GameObject NPC)
    {
        dialogManager.Action(NPC);
        GameManager.Instance.Who = NPC;
        Vector3 P = NPC.transform.position;
        P.y = P.y + 1.5f;
        gameObject.transform.position = P;
        while (sayBox.activeSelf)
        {
            yield return null;
        }
        GameManager.Instance.OpenRune();

    }
    IEnumerator BlackSmithNPCmeet(GameObject NPC)
    {
        dialogManager.Action(NPC);
        GameManager.Instance.Who = NPC;
        Vector3 P = NPC.transform.position;
        P.x = P.x - 1.5f;
        gameObject.transform.position = P;
        while (sayBox.activeSelf)
        {
            yield return null;
        }
        GameManager.Instance.OpenReinForce();

    }
    IEnumerator SHOPNPCmeet(GameObject NPC)
    {
        dialogManager.Action(NPC);
        GameManager.Instance.Who = NPC;
        Vector3 P = NPC.transform.position;
        P.x = P.x + 1.5f;
        gameObject.transform.position = P;
        while (sayBox.activeSelf)
        {
            yield return null;
        }
        GameManager.Instance.OpenSHOP();
    }

}
