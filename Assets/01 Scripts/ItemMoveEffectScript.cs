using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMoveEffectScript : MonoBehaviour
{
    [SerializeField] float itemMoveChangeTime;
    [SerializeField] float itemMoveSpeed = 1f;

    private Vector2 itemMoveDir;

    private void Start()
    {
        itemMoveDir = Vector2.up;
        StartCoroutine(ChangeMove(itemMoveChangeTime));
    }

    private void Update()
    {
        transform.Translate(itemMoveDir * itemMoveSpeed * Time.deltaTime);
    }
    IEnumerator ChangeMove(float _time)
    {
        while (true)
        {
            yield return new WaitForSeconds(_time);
            itemMoveDir = -itemMoveDir;
        }
    }
}
