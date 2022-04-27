using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float groundRadius = 0.15f;
    [SerializeField] float throwPower = 5f;

    bool isGrounded
    {
        get
        {
            return anim.GetBool("isGrounded");
        }

        set
        {
            anim.SetBool("isGrounded", value);
        }
    }

    int jumpCount;
    bool isLockControl;
    bool isLockControlForce;

    readonly int MAX_JUMP_COUNT = 2;

    void Update()
    {
        CheckGround();

        if (!player.isDead && !isLockControl && !isLockControlForce)
        {
            Move();
            Jump();
        }

        anim.SetFloat("VelocityY", rigid.velocity.y);
        anim.SetInteger("jumpCount", jumpCount);
    }

    void CheckGround()
    {
        // 상승중일 때는 땅에 있지 않다
        if (rigid.velocity.y > 0.0f)
        {
            isGrounded = false;
            return;
        }

        //광선을 쏨
        //pivot(원점)은 내 위치, 방향은 아래쪽, 거리는 0.3
        //groundMask에 해당하는 레이어에 속하는 충돌체를 감지
        //hit은 내가 충돌한 '충돌체'의 정보를 들고있음
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRadius, groundMask);
        if (hit.collider != null)
        {
            isGrounded = true;
            isLockControl = false;
            jumpCount = MAX_JUMP_COUNT;
        }
    }

    void Move()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(x * speed, rigid.velocity.y);

        anim.SetInteger("horizontal", x);
        if (x != 0)
            spriteRenderer.flipX = (x == -1);
    }

    void Jump()
    {
        //KeyDown : 키 입력하는 순간 한 번
        //KeyUp : 키 떼는 순간 한 번
        //Key : 누르고 있는 동안 계속
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount>0)
        {
            //Regidbody2D.AddForce(Vector3, ForceMode2D) : Void
            //Vector3 방향 + 힘으로 힘을 가함
            //ForceMode2D.Force : 민다
            //ForceMode2D.Impulse : 폭발적인 힘 가함
            rigid.velocity = new Vector2(rigid.velocity.x, 0f);
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("onJump");
            jumpCount -= 1;
            AudioManager.Instance.PlaySE("jump");
        }
    }

    public void OnThrow(Transform targetPivot)
    {
        OnThrow(targetPivot.position);
    }

    public void OnThrow(Vector3 targetPosition)
    {
        //내 위치 - 상대방의 위치 = 상대방에서 내 위치로 보는 방향
        Vector3 direction = transform.position - targetPosition;
        direction.Normalize(); //벡터값 정규화
        direction.y = 1;       //y축 벡터 제거

        rigid.velocity = Vector2.zero;
        rigid.AddForce(direction * throwPower, ForceMode2D.Impulse);
        isLockControl = true;
    }

    public void OnSwitchLockControl(bool isLock)
    {
        isLockControlForce = isLock;

        if(isLock)
        {
            rigid.velocity = new Vector2(0f, rigid.velocity.y);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundRadius);
    }
}