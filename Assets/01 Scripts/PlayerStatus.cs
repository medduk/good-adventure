using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] Slider playerHpSlider;
    Animator animator;
    Rigidbody2D rigidbody2D;

    [SerializeField] int playerMaxHp = 100;
    [SerializeField] int playerHp = 100;
    [SerializeField] int playerDamage = 10;
    [SerializeField] float playerMoveSpeed = 2f;
    [SerializeField] float playerAttackDelay = 1f;

    private bool stopDamage = false;

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
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        playerHp = playerMaxHp;
        playerHpSlider.value = 1f;
    }

    public void TakeDamage(int damage)
    {
        if (!stopDamage)
        {
            playerHp -= damage;
            playerHpSlider.value = (float)playerHp / playerMaxHp;
            animator.SetTrigger("isHit");
            StartCoroutine(StopDamage());
        }
    }

    IEnumerator StopDamage()
    {
        stopDamage = true;
        rigidbody2D.mass = rigidbody2D.mass * 10f;
        yield return new WaitForSeconds(1f);
        stopDamage = false;
        rigidbody2D.mass = rigidbody2D.mass * 0.1f;
    }

}
