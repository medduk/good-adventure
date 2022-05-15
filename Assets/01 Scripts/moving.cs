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
    private float x, y;     // 이동계산 좌표
    #endregion
    //Github test
    #region Public
    //public float movespeed = 2f;
    public Joystick joystick;
    public ParticleSystem playerWalkingParticle;

    public ParticleSystem playerAfterimageParticle;
    public bool onAfterimage;

    private bool isGameOver = false;

    public GameObject talking;  // 대화중을 나타내는 변수
    #endregion
    private void Awake()
    {
        rigibody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        isGameOver = false;
    }

    private void Start()
    {
        StartCoroutine(OnWalkingParticle());
        StartCoroutine(OnAtferimageParticle());
    }

    void Update()  
    {
        if (isGameOver)
        {
            animator.SetBool("IsWalking", false);
            return;
        }
        if (Time.timeScale == 0)
        {
            return;
        }
        rigibody2D.velocity = Vector2.zero;

#if UNITY_EDITOR
        x = joystick.Horizontal + Input.GetAxisRaw("Horizontal");
        y = joystick.Vertical + Input.GetAxisRaw("Vertical");
#else
        x = joystick.Horizontal;
        y = joystick.Vertical;
#endif
        if (x != 0 || y != 0)
        {
            if (!talking.activeSelf)
            {
                isMoving = true;
                animator.SetBool("IsWalking", true);
            }
        }
        else
        { 
            isMoving = false;
            animator.SetBool("IsWalking", false);
        }
        if (talking.activeSelf) // 대화이벤트시 움직임 제한
        {
            isMoving = false;
            animator.SetBool("IsWalking", false);
        }
    }
    private void FixedUpdate()
    {
        if(isGameOver)
        {
            return;
        }
        if (isMoving)
        {
            Move(x);
        }
    }
    private void Move(float xDirection)  
    {
        Vector2 inputPosition = new Vector2(x, y);
        if (!talking.activeSelf) // 대화이벤트시 움직임 제한
        {
            if (xDirection < 0)  // 이동방향에 따라 캐릭터 좌우 반전 ㄴ
            {
                spriteRenderer.flipX = false;
                playerAfterimageParticle.GetComponent<ParticleSystemRenderer>().flip = Vector3.zero;
            }

            if (xDirection > 0)
            {
                spriteRenderer.flipX = true;
                playerAfterimageParticle.GetComponent<ParticleSystemRenderer>().flip = Vector3.right;
            }
            rigibody2D.MovePosition(rigibody2D.position + inputPosition * PlayerStatus.Instance.PlayerMoveSpeed * Time.fixedDeltaTime);
        }
    }
    public void SetGameOver(bool _isGameOver)    // 게임 오버 시 ture, 부활 같은 계속 진행 시 false
    {
        isGameOver = _isGameOver;
    }

    IEnumerator OnWalkingParticle()
    {
        while (true)
        {
            yield return new WaitUntil(() => animator.GetBool("IsWalking"));
            playerWalkingParticle.Play();
            yield return new WaitUntil(() => !animator.GetBool("IsWalking"));
            playerWalkingParticle.Stop();
        }
    }
    IEnumerator OnAtferimageParticle()
    {
        while (true)
        {
            yield return new WaitUntil(() => animator.GetBool("IsWalking"));
            if (onAfterimage)
            {
                playerAfterimageParticle.Play();
            }
            yield return new WaitUntil(() => !animator.GetBool("IsWalking"));
            if(playerAfterimageParticle.isPlaying) playerAfterimageParticle.Stop();
        }
    }
}
