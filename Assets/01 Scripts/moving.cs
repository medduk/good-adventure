using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    // Start is called before the first frame update
    #region Private
    private Rigidbody2D rigibody2D;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    private bool isMoving = false; // 키 입력 받을때 움직임 체크 용도
    private float x, y;
    #endregion
    //Github test
    #region Public
    public float movespeed = 2f;
    #endregion
    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rigibody2D.velocity = Vector2.zero;
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        if (x != 0 || y != 0)
        {
            isMoving = true;
            animator.SetBool("IsWalking", true);
        }
        else 
        { 
            isMoving = false;
            animator.SetBool("IsWalking", false);
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
        Vector2 inputPosition = new Vector2(x, y);
        if (xDirection < 0)
            spriteRenderer.flipX = false;
  
        if(xDirection > 0)
            spriteRenderer.flipX = true;

        rigibody2D.MovePosition(rigibody2D.position + inputPosition * movespeed * Time.fixedDeltaTime);

    }
}
