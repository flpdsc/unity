using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteraction
{
    string GetContext(); //표시할 내용
    void OnInteraction(); //상호작용 
}

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

    [Header("Search")]
    [SerializeField] LayerMask searchMask; //탐색 마스크 
    [SerializeField] float searchRadius; //탐색 범위 
    [SerializeField] KeyCode interactionKey; //상호작용 키 

    IInteraction interaction; //상호작용가능한 대상  
    bool isAim;

    private void Start()
    {
        inven.AddItem(ItemManager.Instance.GetItem("Armor", 1));
        inven.AddItem(ItemManager.Instance.GetItem("Potion", 10));
        inven.AddItem(ItemManager.Instance.GetItem("Helmet", 2));
    }

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

        //상호작용 키 누름 
        if(Input.GetKeyDown(interactionKey))
        {
            if(interaction!=null)
            {
                interaction.OnInteraction();
            }    
        }

        ChageFireType();
        Aim();
        SearchInteraction();
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

    private void SearchInteraction()
    {
        interaction = null;

        //최초에 직선 레이를 발사해 상호작용 물체 검색 
        RaycastHit hit;
        if(Physics.Raycast(eye.transform.position, eye.transform.forward, out hit, searchRadius * 2f, searchMask))
        {
            interaction = hit.collider.GetComponent<IInteraction>();
        }

        //실패하면 원형으로 재검색 
        if(interaction == null)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius, searchMask);
            if(colliders.Length > 0)
            {
                for(int i=0; i<colliders.Length; ++i)
                {
                    IInteraction target = colliders[i].GetComponent<IInteraction>();
                    if(target != null)
                    {
                        interaction = target;
                        break;
                    }
                }
            }
        }

        //상호작용 가능한 물체를 찾았다면 
        if(interaction!=null)
        {
            InteractionUI.Instance.Setup(interactionKey, interaction);
        }
        else
        {
            InteractionUI.Instance.Close();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(eye.transform.position, eye.transform.forward * searchRadius * 2f);


    }
}
 