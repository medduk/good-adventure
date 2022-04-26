using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoFirstStage : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite opendoor;
    [SerializeField] GameObject nowtalk;
    public DialogManager dialogManager;
    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            boxCollider2D.enabled = false;
            nowtalk = GameObject.Find("GameUI").transform.Find("SayBox").gameObject;
            dialogManager = GameObject.Find("DialogManager").GetComponent<DialogManager>();
            StartCoroutine(OpenDoorStage1(collision.gameObject));
        }
    }

    IEnumerator OpenDoorStage1(GameObject player)
    {
        dialogManager.Action(gameObject);
        Vector3 P = gameObject.transform.position;
        P.y = P.y - 1.5f;
        player.transform.position = P;
        while (nowtalk.activeSelf)
        {
            yield return null;
        }
        spriteRenderer.sprite = opendoor;
        dialogManager.Action(gameObject, 1);
    }
}
