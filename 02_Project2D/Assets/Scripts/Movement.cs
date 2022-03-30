using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] Animator anim;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D rigid;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpPower = 5f;

    bool isGrounded;
    bool isJumping;

    private void Start()
    {
    }

    private void Update()
    {
        CheckGround();
        Move();
        Jump();
    }

    void CheckGround()
    {
        //광선을 쏨
        //pivot(원점)은 내 위치, 방향은 아래쪽, 거리는 0.3
        //groundMask에 해당하는 레이어에 속하는 충돌체를 감지
        //hit은 내가 충돌한 '충돌체'의 정보를 들고있음
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.3f, groundMask);
        if(hit.collider != null)
        {
            isGrounded = true;
            isJumping = false;
        }
        else
        {
            isGrounded = false;
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
        if(Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded)
        {
            //Regidbody2D.AddForce(Vector3, ForceMode2D) : Void
            //Vector3 방향 + 힘으로 힘을 가함
            //ForceMode2D.Force : 민다
            //ForceMode2D.Impulse : 폭발적인 힘 가함
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJumping = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.down * 0.3f);
    }
}