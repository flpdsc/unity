using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : Trap
{
    [SerializeField] float delayTime = 4; //딜레이 시간 
    [SerializeField] float continueTime = 3; //지속시간 

    Animator anim;
    bool isOn;
    bool isFire;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isOn = false;
        isFire = false;
    }

    public override void OnContactEnter(GameObject gameObject)
    {
        if (isOn)
            return;

        isOn = true;
        anim.SetTrigger("switch");

        //delayTime 후에 OnStartTrap 함수를 호출하라 
        Invoke(nameof(OnStartFire), delayTime);
    }
    
    public override void OnContactStay(GameObject target)
    {
        if(!isFire)
        {
            return;
        }

        Player player = target.GetComponent<Player>();
        if(player!=null)
        {
            player.OnContactTrap(gameObject);
        }
    }

    private void OnStartFire()
    {
        anim.SetTrigger("onFire");
        isFire = true;
        Invoke(nameof(OnStopFire), continueTime);
    }

    private void OnStopFire()
    {
        anim.SetTrigger("offFire");
        isOn = false;
        isFire = false;
}
    }
