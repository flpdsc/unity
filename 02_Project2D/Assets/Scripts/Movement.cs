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

    bool isJumping;
    bool isLockControl;

    void Update()
    {
        CheckGround();

        if (!player.isDead && !isLockControl)
        {
            Move();
            Jump();
        }

        anim.SetFloat("VelocityY", rigid.velocity.y);
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
            isJumping = false;
            isLockControl = false;
        }
    }

    void Move()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");
        transform.Translate(Vector3.right * x * speed * Time.deltaTime);

        anim.SetInteger("horizontal", x);
        if (x != 0)
            spriteRenderer.flipX = (x == -1);
    }

    void Jump()
    {
        //KeyDown : 키 입력하는 순간 한 번
        //KeyUp : 키 떼는 순간 한 번
        //Key : 누르고 있는 동안 계속
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded)
        {
            //Regidbody2D.AddForce(Vector3, ForceMode2D) : Void
            //Vector3 방향 + 힘으로 힘을 가함
            //ForceMode2D.Force : 민다
            //ForceMode2D.Impulse : 폭발적인 힘 가함
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    public void OnThrow(Transform targetPivot)
    {
        //내 위치 - 상대방의 위치 = 상대방에서 내 위치로 보는 방향
        Vector3 direction = transform.position - targetPivot.position;
        direction.Normalize(); //벡터값 정규화
        direction.y = 1;       //y축 벡터 제거

        //direction 방향으로 throwPower만큼 (한번에) 힘을 가함
        rigid.AddForce(direction * throwPower, ForceMode2D.Impulse);
        isLockControl = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundRadius);
    }
}