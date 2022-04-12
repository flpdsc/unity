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
    bool isJumping;
    bool isLockControl;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        //플레이어가 죽지 않았고 컨트롤이 막히지 않았을 경우
        if(!player.isDead && !isLockControl)
        {
            Move();
            Jump();
        }

        //Rigidbody2D에 현재 오브젝트의 속력에 관한 변수가 있음
        //Vector2 rigid.velocity;
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
        //pivot(원점)은 내 위치이고, 방향은 아래쪽, 거리는 0.3, groundMask에 해당하는 레이어인 충돌체를 감지
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
        //KeyDown : 키 입력하는 순간 1번
        //KeyUp : 키 떼는 순간 1번
        //Key : 누르고 있는 동안 계속
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded) 
        {
            //Regidbody2D.AddForce(Vector3, ForceMode2D) : void
            //=>Vector3 방향 + 힘으로 힘을 가함
            //=>ForceMode2D.Force : 민다
            //=>ForceMode2D.Impulse : 폭발적인 힘을 가함
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySE("jump");
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
