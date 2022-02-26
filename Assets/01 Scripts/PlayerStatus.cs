using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    [SerializeField] Slider playerHpSlider;

    [SerializeField] int playerMaxHp = 100;
    [SerializeField] int playerHp;
    [SerializeField] int playerDamage = 10;
    [SerializeField] float playerMoveSpeed = 2f;
    [SerializeField] float playerAttackDelay = 1f;    

    [SerializeField] float playerDefense;

    private bool stopDamage = false;

    private static PlayerStatus instance = null;
    private Animator animator;
    private new Rigidbody2D rigidbody2D;

    private bool isGameOver = false;

    public DialogManager dialogManager;

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

        isGameOver = false;
    }
    private void Start()
    {
        playerHp = playerMaxHp;
        playerHpSlider.value = 1f;  // Make Full Hp when the game gets started.
    }

    private void Update()
    {
        playerHpSlider.value = Mathf.Lerp(playerHpSlider.value, (float)playerHp / playerMaxHp, Time.deltaTime * 5f);
    }
    public void TakeDamage(int damage)
    {
        if (!stopDamage)
        {
            playerHp -= damage;

            animator.SetTrigger("IsHit");
            if (playerHp <= 0 && !isGameOver)
            {
                isGameOver = true;
                GameManager.Instance.SetGameOver();
                gameObject.GetComponent<moving>().SetGameOver();
                gameObject.GetComponent<MainFmAttack>().SetGameOver();
            }
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Portal")
        {
            StageManager.Instance.MoveNextStage();
        }

        if (collision.transform.tag == "Tutorial")
        {
           
            Destroy(collision);
            dialogManager.Action(collision.gameObject);
        }
    }
}
