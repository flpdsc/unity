using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float GodModeTime;
    [SerializeField] int maxHp; //�÷��̾��� �ִ� ü��

    //������ �������� ĳ���ؼ� ���� ���� ����
    //GetComponent�� ������Ʈ�� �˻��ϱ� ������ �޸� ȿ���� ���� ����
    //���� �ʱ�ȭ ������ �̸� �˻��ϰ� ���Ŀ��� ������ �����
    SpriteRenderer spriteRenderer;
    bool isGodMode;
    bool isFallDown; //�÷��̾ �Ʒ��� ��������
    int hp;   //���� ü��
    int coin; //���� ����

    //������Ƽ
    public bool isDead => (hp <= 0) || isFallDown; //isDead�� ������ �����ϸ� ���ϰ��� �Ʒ� ���� �������� ��� ����
    public int Hp => hp; //Hp�� �����ϸ� hp���� ����
    public int Coin => coin; //Coin�� �����ϸ� coin���� ����

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        hp = maxHp;
    }
    public void OnContactTrap(TrapSpike trap)
    {
        if (isGodMode)
        {
            return;
        }
        //�� ������Ʈ���� Movement �˻�
        //���� OnThrow�Լ��� trap�� transform���� 
        Movement movement = gameObject.GetComponent<Movement>();
        movement.OnThrow(trap.transform);
        //Debug.Log($"{trap.name}�� �浹��");

        OnHit();
    }

    public void OnContactCoin(Coin target)
    {
        coin += 1;
    }

    public void OnFallDown()
    {
        isFallDown = true;
    }

    private void OnHit()
    {
        if (isGodMode)
        {
            return;
        }

        if ((hp -= 1) <= 0) //ü���� 1 ���� �� 0 ���϶��,
        {
            OnDead(); //����
        }
        else
        {
            isGodMode = true;
            spriteRenderer.color = new Color(1, 1, 1, 0.5f); //������ ����

            //Invoke(string, int) : void
            //Ư���Լ��� n�� �Ŀ� ȣ��

            //nameof(Method) : �Լ����� string ���ڷ� ��ȯ
            Invoke(nameof(ReleaseGodMode), GodModeTime);
            StartCoroutine(HitPlayer());
        }
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
