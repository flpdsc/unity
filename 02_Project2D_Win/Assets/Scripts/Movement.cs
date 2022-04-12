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
        //�÷��̾ ���� �ʾҰ� ��Ʈ���� ������ �ʾ��� ���
        if(!player.isDead && !isLockControl)
        {
            Move();
            Jump();
        }

        //Rigidbody2D�� ���� ������Ʈ�� �ӷ¿� ���� ������ ����
        //Vector2 rigid.velocity;
        anim.SetFloat("VelocityY", rigid.velocity.y);
    }

    void CheckGround()
    {
        // ������� ���� ���� ���� �ʴ�
        if (rigid.velocity.y > 0.0f)
        {
            isGrounded = false;
            return;
        }

        //������ ��
        //pivot(����)�� �� ��ġ�̰�, ������ �Ʒ���, �Ÿ��� 0.3, groundMask�� �ش��ϴ� ���̾��� �浹ü�� ����
        //hit�� ���� �浹�� '�浹ü'�� ������ �������
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
        //KeyDown : Ű �Է��ϴ� ���� 1��
        //KeyUp : Ű ���� ���� 1��
        //Key : ������ �ִ� ���� ���
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && isGrounded) 
        {
            //Regidbody2D.AddForce(Vector3, ForceMode2D) : void
            //=>Vector3 ���� + ������ ���� ����
            //=>ForceMode2D.Force : �δ�
            //=>ForceMode2D.Impulse : �������� ���� ����
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            AudioManager.Instance.PlaySE("jump");
            isJumping = true;
        }
    }

    public void OnThrow(Transform targetPivot)
    {
        //�� ��ġ - ������ ��ġ = ���濡�� �� ��ġ�� ���� ����
        Vector3 direction = transform.position - targetPivot.position;
        direction.Normalize(); //���Ͱ� ����ȭ
        direction.y = 1;       //y�� ���� ����

        //direction �������� throwPower��ŭ (�ѹ���) ���� ����
        rigid.AddForce(direction * throwPower, ForceMode2D.Impulse);
        isLockControl = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * groundRadius);
    }
}
