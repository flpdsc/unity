using System.Collections;
using UnityEngine;

public class Player : Singleton<Player>
{
    [SerializeField] float GodModeTime;
    [SerializeField] int maxHp; //�÷��̾��� �ִ� ü��

    //������ �������� ĳ���ؼ� ���� ���� ����
    //GetComponent�� ������Ʈ�� �˻��ϱ� ������ �޸� ȿ���� ���� ����
    //���� �ʱ�ȭ ������ �̸� �˻��ϰ� ���Ŀ��� ������ �����
    SpriteRenderer spriteRenderer;
    Movement movement;
    Animator anim;

    bool isGodMode;
    bool isFallDown; //�÷��̾ �Ʒ��� ��������
    int hp;   //���� ü��

    //������Ƽ
    public bool isDead => (hp <= 0) || isFallDown; //isDead�� ������ �����ϸ� ���ϰ��� �Ʒ� ���� �������� ��� ����
    public int Hp => hp; //Hp�� �����ϸ� hp���� ����

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = gameObject.GetComponent<Movement>();
        anim = GetComponent<Animator>();

        hp = maxHp;

        StartPoint.Instance.SetStartPoint(transform);
    }
    
    public void OnContactTrap(GameObject trap)
    {
        if (isGodMode)
        {
            return;
        }
        StartCoroutine(OnHit(trap.transform));
    }

    public void OnContactCoin(Coin target)
    {
        GameManager.Instance.AddEatCount(1);
    }

    public void OnFallDown()
    {
        isFallDown = true;
    }

    public void OnSwitchLockControl(bool isLock)
    {
        movement.OnSwitchLockControl(isLock);
    }

    public void OnEndHitAnim()
    {
        anim.SetBool("isHit", false);
    }

    private IEnumerator OnHit(Transform target)
    {
        if ((hp -= 1) <= 0) //ü���� 1 ���� �� 0 ���϶��,
        {
            OnDead(); //����
            Collider2D collider = GetComponent<Collider2D>(); //�ݶ��̴� �˻�
            collider.isTrigger = true; //Ʈ���ŷ� ����
        }

        isGodMode = true;
        spriteRenderer.color = new Color(1, 1, 1, 0.5f); //������ ����
        anim.SetBool("isHit", true);
        anim.SetTrigger("onHit"); //�ǰ� �ִϸ��̼� Ʈ����

        //Invoke(string, int) : void
        //Ư���Լ��� n�� �Ŀ� ȣ��

        //nameof(Method) : �Լ����� string ���ڷ� ��ȯ
        Invoke(nameof(ReleaseGodMode), GodModeTime);
        StartCoroutine(HitPlayer());
        //�� ������Ʈ���� Movement �˻�
        //���� OnThrow�Լ��� trap�� transform���� ���� ȣ��
        yield return null;
        movement.OnThrow(target);
    }

    private void OnDead()
    {

    }

    private void ReleaseGodMode()
    {
        isGodMode = false;  //��������
        spriteRenderer.color = Color.white; //�������� �ǵ���
    }

    //�ڷ�ƾ
    private IEnumerator HitPlayer()
    {
        Color red = new Color(1, 0, 0, 0.5f);
        Color white = new Color(1, 1, 1, 0.5f);

        for(int i=0; i<3; ++i)
        {
            spriteRenderer.color = red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = white;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
