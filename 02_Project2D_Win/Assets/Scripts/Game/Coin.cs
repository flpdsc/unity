using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    Animator anim;
    new Collider2D collider2D;

    private void Start()
    {
        anim = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�÷��̾ ���� �ε�����
        Player player = collision.GetComponent<Player>();
        if(player != null)
        {
            player.OnContactCoin(this); //�÷��̾�� �˷��ֱ�
            collider2D.enabled = false; //�浹ü ����
            anim.SetTrigger("onEat");   //onEatƮ���� ������
            AudioManager.Instance.PlaySE("eat");
        }
    }

    private void OnDestroyCoin()
    {
        Destroy(gameObject); //Coin ��ũ��Ʈ�� �����ϴ� ���� ������Ʈ�� ����
    }
}
