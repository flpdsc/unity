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
        //Tiem.deltaTime : 이전 프레임부터 현재 프레임까지 걸린 시간
        //time : 시간의 흐름을 나타내는 float형 변수
        //anim.isPlaying <bool> : 현재 애니메이션을 재생하고 있는가?
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

    //트리거가 충돌하는 그 순간 한 번 호출
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.OnContactTrap(this);
        }
    }
}
