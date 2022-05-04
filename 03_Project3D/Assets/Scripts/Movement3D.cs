using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [Header("Value")]
    [SerializeField] Animator anim;
    [SerializeField] float moveSpeed = 5; //이동 속도 
    [SerializeField] float jumpHeight = 3; //점프 높이 
    [Range(1.0f, 3.0f)]
    [SerializeField] float gravityScale = 2.95f; //중력 배수 

    [Header("Ground")]
    [SerializeField] Transform groundPivot; //지면 체크 중심점 
    [SerializeField] float groundRadius; //지면 체크 구의 반지름 
    [SerializeField] LayerMask groundMask; //지면 레이어 마스크 

    CharacterController controller; //캐릭터 컨트롤러 클래스
    bool isGrounded;
    Vector3 velocity; //현재 플레이어 속도 

    float gravity => -9.81f * gravityScale; //실제 중력 가속도 * 중력 배수 

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundPivot.position, groundRadius, groundMask);
        anim.SetBool("isGrounded", isGrounded);

        //지면에 도달했지만 여전히 속도가 하강하고 있을 때 
        if(isGrounded && velocity.y<0f)
        {
            //작은 값을 줘서 착지할 수 있도록 함 
            velocity.y = -2f;
        }

        Move();

        //점프 
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            //(H * -2f * G)^2 물리공식에 의해 Vector.up 방향으로 속도를 가함 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            anim.SetTrigger("onJump");
        }

        //계속 중력을 받기 때문에 중력값을 더함 
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal"); //오른쪽 : 1, 왼쪽 : -1
        float z = Input.GetAxis("Vertical"); //위쪽 : 1, 아래쪽 -1

        //Vector3.right : 월드 좌표 기준으로 오른쪽 방향값
        //transform.right : 나를 기준으로 오른쪽 방향값

        //direction : 내 키 입력에 따라 가고자 하는 방향
        //movement : 이동량 
        //방향에 -1을 곱하면 반대가 됨
        Vector3 direction = (transform.right * x) + (transform.forward * z);
        controller.Move(direction * moveSpeed * Time.deltaTime);

        anim.SetFloat("horizontal", x);
        anim.SetFloat("vertical", z);
    }

    private void OnDrawGizmos()
    {
        if (groundPivot == null)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundPivot.position, groundRadius);
    }

}
