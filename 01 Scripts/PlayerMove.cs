using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /* 모든 캐릭터가 공통으로 사용. */
    /* 추후에 터치 메소드로 바꿔야함. */
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isMoving = false;  // 키 입력 받을때 움직임 체크용.
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

        rigidbody2D.velocity = Vector2.zero;    // 혹시나 모를 상황을 대비해서
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (x != 0 || y != 0) isMoving = true;  // 이동하면 FixedUpdate에서 실행
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

        if (xDirection < 0)  // 왼쪽 이동
        {
            spriteRenderer.flipX = false;   // 왼쪽 바라봄.
        }
        if(xDirection > 0)  // 오른쪽 이동
        {
            spriteRenderer.flipX = true;    // 오른쪽 바라봄.
        }
        rigidbody2D.MovePosition(rigidbody2D.position + inputPosition * moveSpeed * Time.fixedDeltaTime);

    }

}