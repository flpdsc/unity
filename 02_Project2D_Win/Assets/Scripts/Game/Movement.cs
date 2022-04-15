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
        //�÷��̾ ���� �ʾҰ� ��Ʈ���� ������ �ʾ��� ���
        if(!player.isDead && !isLockControl && !isLockControlForce)
        {
            Move();
            Jump();
        }

        //Rigidbody2D�� ���� ������Ʈ�� �ӷ¿� ���� ������ ����
        //Vector2 rigid.velocity;
        anim.SetFloat("VelocityY", rigid.velocity.y);
        anim.SetInteger("jumpCount", jumpCount);
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
            isLockControl = false;
            jumpCount = MAX_JUMP_COUNT; //���� ���� ����� �� ���� ī��Ʈ Ƚ���� �ִ�� ����
        }
    }

    void Move()
    {
        int x = (int)Input.GetAxisRaw("Horizontal");

        //Translate�� �����̵��̱� ������ ���� ó������ �ڿ������� ����
        //���� Velocity(�ӷ�)�� �̿��� ĳ���͸� �̵���Ŵ
        rigid.velocity = new Vector2(x*speed, rigid.velocity.y);
        //transform.Translate(Vector3.right * x * speed * Time.deltaTime);
        anim.SetInteger("horizontal", x);
        if (x != 0)
            spriteRenderer.flipX = (x == -1);
    }

    void Jump()
    {
        //KeyDown : Ű �Է��ϴ� ���� 1��
        //KeyUp : Ű ���� ���� 1��
        //Key : ������ �ִ� ���� ���

        //����Ű�� ������ ����Ƚ���� 0���� Ŭ ��
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount>0) 
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0f); //������Ʈ�� �ӵ��� x���� �״�� y���� �״�� ����(2������ �� ���� ���� �����ϵ��� �ϱ� ����)
            //Regidbody2D.AddForce(Vector3, ForceMode2D) : void
            //=>Vector3 ���� + ������ ���� ����
            //=>ForceMode2D.Force : �δ�
            //=>ForceMode2D.Impulse : �������� ���� ����
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetTrigger("onJump");
            jumpCount -= 1;
            AudioManager.Instance.PlaySE("jump");
        }
    }

    public void OnThrow(Transform targetPivot)
    {
        //�� ��ġ - ������ ��ġ = ���濡�� �� ��ġ�� ���� ����
        Vector3 direction = transform.position - targetPivot.position;
        direction.Normalize(); //���Ͱ� ����ȭ
        direction.y = 1;       //y�� ���� ����

        rigid.velocity = Vector2.zero; //������ �ӵ��� 0���� ���� (�����ϰ� ����)
        rigid.AddForce(direction * throwPower, ForceMode2D.Impulse); //direction �������� throwPower��ŭ (�ѹ���) ���� ����
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
