using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapFire : MonoBehaviour
{
    [SerializeField] GameObject fireCollider;
    [SerializeField] float delayTime = 4;
    [SerializeField] float continueTime = 3;

    Animator anim;
    bool isOn;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isOn = false;
        fireCollider.SetActive(false);
    }

    public void OnSwitchTrap()
    {
        if(isOn)
        {
            return;
        }
        Debug.Log("fire!");
        anim.SetTrigger("switch");
        isOn = true;

        Invoke(nameof(OnStartTrap), delayTime);
    }

    public void OnContactFire(GameObject target)
    {
        Player player = target.GetComponent<Player>();
        if(player!=null)
        {
            player.OnContactTrap(target);
        }
    }

    private void OnStartTrap()
    {
        fireCollider.SetActive(true);
        anim.SetTrigger("on");
        Invoke(nameof(OnStopTrap), continueTime);
    }

    private void OnStopTrap()
    {
        anim.SetTrigger("off");
        isOn = false;
    }
}
