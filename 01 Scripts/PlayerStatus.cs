using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] int playerHp = 100;
    [SerializeField] int playerDamage = 10;
    [SerializeField] float playerMoveSpeed = 2f;
    [SerializeField] float playerAttackDelay = 1f;

    Animator animator;

    public int GetPlayerHp()
    {
        return playerHp;
    }
    
    public int GetPlayerDamage()
    {
        return playerDamage;
    }
    public float GetPlayerMoveSpeed()
    {
        return playerMoveSpeed;
    }

    public float GetPlayerAttackDelay()
    {
        return playerAttackDelay;
    }

    /* 구현 해야함 */
    [SerializeField] float playerDefense;

    private static PlayerStatus instance = null;

    public static PlayerStatus Instance
    {
        get
        {
            if (instance != null) return instance;
            return null;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        playerHp -= damage;
        animator.SetTrigger("isHit");
    }
}
