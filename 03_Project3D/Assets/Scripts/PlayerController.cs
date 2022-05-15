using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] Animator anim; //애니메이터
    [SerializeField] WeaponController weapon; //무기 

    private void Update()
    {
        if (weapon != null)
        {
            Fire();
            Reload();
            ChageFireType();
        }
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            if (weapon.StartFire())
            {
                anim.SetTrigger("onFire");
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.EndFire();
        }
    }

    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && weapon.Reload())
        {
            anim.SetTrigger("onReload");
        }
    }

    private void ChageFireType()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            weapon.OnChangeType();
        }
    }
}
 