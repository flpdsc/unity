using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] Animator anim; //애니메이터
    [SerializeField] WeaponController weapon; //무기
    [SerializeField] GrenadeThrow grenadeThrow; //수류탄
    [SerializeField] Inventory inven; //인벤토리 

    [Header("Eye")]
    [SerializeField] Camera eye; //눈
    [SerializeField] Transform normalCamera; //일반 시야 위치 
    [SerializeField] Transform aimCamera; //에임 시야 위치 

    bool isAim;

    private void Update()
    {
        if (weapon != null && !weapon.isReload && !InventoryUI.Instance.isOpen)
        {
            Fire();
            Reload();
            Grenade();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            //인벤토리가 열렸으면 마우스 풀고 닫히면 락 
            bool isOpen = InventoryUI.Instance.SwitchInventory();
            if(isOpen)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Item item = ItemManager.Instance.GetItem("Potion", 20);
            inven.AddItem(item);
        }

        ChageFireType();
        Aim();
    }

    private void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            if (weapon.StartFire(isAim))
            {
                anim.SetTrigger("onFire");
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            weapon.EndFire();
        }
    }

    private void Aim()
    {
        if(Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("onAim");
        }
        isAim = Input.GetMouseButton(1) && !weapon.isReload;
        anim.SetBool("isAim", isAim);
        eye.transform.position = isAim ? aimCamera.position : normalCamera.position;
        eye.fieldOfView = isAim ? 45 : 60;
        CrossHairUI.Instance.SwitchCrosshair(!isAim);
    }
    private void Reload()
    {
        if (Input.GetKeyDown(KeyCode.R) && weapon.Reload())
        {
            anim.SetTrigger("onReload");
        }
    }

    private void Grenade()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            grenadeThrow.OnThrowGrenade();
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
 