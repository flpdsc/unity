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
        //Tiem.deltaTime : ?? ????? ?? ????? ?? ??
        //time : ??? ??? ???? float? ??
        //anim.isPlaying <bool> : ?? ?????? ???? ????
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

    //???? ???? ? ?? ? ? ??
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.OnContactTrap(this);
        }
    }
}