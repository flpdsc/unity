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
    [SerializeField] float throwPower = 5;

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
    bool isLockControl;
    bool isLockControlForce;
    int jumpCount;

    readonly int MAX_JUMP_COUNT = 2;

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        //플레이어가 죽지 않았고 컨트롤이 막히지 않았을 경우
        if(!player.isDead && !isLockControl && !isLockControlForce)
        {
            Move();
            Jump();
        }

        //Rigidbody2D에 현재 오브젝트의 속력에 관한 변수가 있음
        //Vector2 rigid.velocity;
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
        //pivot(원점)은 내 위치이고, 방향은 아래쪽, 거리는 0.3, groundMask에 해당하는 레이어인 충돌체를 감지
        //hit은 내가 충돌한 '충돌체'의 정보를 들고있음
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRadius, groundMask);
        if (hit.collider != null)
        {
            isGrounded = true;
            isLockControl = false;
            jumpCount = MAX_JUMP_COUNT; //발이 땅에 닿았을 때 점프 카운트 횟수를 최대로 돌림
        }
    }

    void Move()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");

        //Translate는 순간이동이기 때문에 물리 처리에서 자연스럽지 않음
        //따라서 Velocity(속력)을 이용해 캐릭터를 이동시킴
        rigid.velocity = new Vector2(x*speed, rigid.velocity.y);
        //transform.Translate(Vector3.right * x * speed * Time.deltaTime);
        anim.SetInteger("horizontal", x);
        if (x != 0)
            spriteRenderer.flipX = (x == -1);
    }

    void Jump()
    {
        //KeyDown : 키 입력하는 순간 1번
        //KeyUp : 키 떼는 순간 1번
        //Key : 누르고 있는 동안 계속

        //점프키를 누르고 점프횟수가 0보다 클 때
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount>0) 
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0f); //오브젝트의 속도를 x축은 그대로 y축은 그대로 변경(2단점프 시 점프 높이 일정하도록 하기 위함)
            //Regidbody2D.AddForce(Vector3, ForceMode2D) : void
            //=>Vector3 방향 + 힘으로 힘을 가함
            //=>ForceMode2D.Force : 민다
            //=>ForceMode2D.Impulse : 폭발적인 힘을 가함
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("onJump");
            jumpCount -= 1;
            AudioManager.Instance.PlaySE("jump");
        }
    }

    public void OnThrow(Transform targetPivot)
    {
        //내 위치 - 상대방의 위치 = 상대방에서 내 위치로 보는 방향
        Vector3 direction = transform.position - targetPivot.position;
        direction.Normalize(); //벡터값 정규화
        direction.y = 1;       //y축 벡터 제거

        rigid.velocity = Vector2.zero; //기존의 속도를 0으로 만듦 (동일하게 날림)
        rigid.AddForce(direction * throwPower, ForceMode2D.Impulse); //direction 방향으로 throwPower만큼 (한번에) 힘을 가함
        isLockControl = true;
    }

    public void OnSwitchLockControl(bool isLock)
    {
        isLockControlForce = isLock;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundRadius);
    }
}
