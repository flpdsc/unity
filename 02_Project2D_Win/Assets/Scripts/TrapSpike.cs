using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpike : MonoBehaviour
{
    [SerializeField] Animation anim;
    [SerializeField] float DelayHideTime;
    [SerializeField] float DelayShowTime;


    bool isShow;
    float time;


    void Start()
    {
        isShow = true;
        time = 0.0f;
    }

    private void Update()
    {
        //Tiem.deltaTime : ���� �����Ӻ��� ���� �����ӱ��� �ɸ� �ð�
        //time : �ð��� �帧�� ��Ÿ���� float�� ����
        //anim.isPlaying <bool> : ���� �ִϸ��̼��� ����ϰ� �ִ°�?
        if (!anim.isPlaying)
        {
            time += Time.deltaTime;
        }

        if (isShow && time >= DelayShowTime)
        {
            anim.Play("Trap_hide");
            isShow = false;
            time = 0.0f;
        }
        else if (!isShow && time >= DelayHideTime)
        {
            anim.Play("Trap_show");
            isShow = true;
            time = 0.0f;
        }
    }

    //Ʈ���Ű� �浹�ϴ� �� ���� �� �� ȣ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.OnContactTrap(this);
        }
    }
}
