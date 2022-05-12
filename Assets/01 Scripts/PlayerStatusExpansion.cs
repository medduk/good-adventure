using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusExpansion : MonoBehaviour
{
    private static PlayerStatusExpansion instance = null;
    public static PlayerStatusExpansion Instance
    {
        get
        {
            if (instance != null) return instance;
            return null;
        }
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            PlayerStatus.Instance.playerMoveSpeedPer -= 0.3f;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            PlayerStatus.Instance.playerMoveSpeedPer += 0.3f;
        }
    }
}
