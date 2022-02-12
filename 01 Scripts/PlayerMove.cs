using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /* ��� ĳ���Ͱ� �������� ���. */
    /* ���Ŀ� ��ġ �޼ҵ�� �ٲ����. */
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isMoving = false;  // Ű �Է� ������ ������ üũ��.
    private float x, y;

    public float moveSpeed = 2f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        isMoving = false;
    }

    void Update()
    {
        //raycastHit2D = Physics2D.Raycast(transform.position, )

        rigidbody2D.velocity = Vector2.zero;    // Ȥ�ó� �� ��Ȳ�� ����ؼ�
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (x != 0 || y != 0) isMoving = true;  // �̵��ϸ� FixedUpdate���� ����
        else
        {
            isMoving = false;
            animator.SetBool("isWalking", false);
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            Move(x);
        }
    }
    private void Move(float xDirection)
    {
        animator.SetBool("isWalking", true);

        Vector2 inputPosition = new Vector2(x, y);

        if (xDirection < 0)  // ���� �̵�
        {
            spriteRenderer.flipX = false;   // ���� �ٶ�.
        }
        if(xDirection > 0)  // ������ �̵�
        {
            spriteRenderer.flipX = true;    // ������ �ٶ�.
        }
        rigidbody2D.MovePosition(rigidbody2D.position + inputPosition * moveSpeed * Time.fixedDeltaTime);

    }

}