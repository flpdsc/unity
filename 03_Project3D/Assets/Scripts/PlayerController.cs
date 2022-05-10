using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Weapon
{
    [SerializeField] int maxHaveBullet; //내가 최대로 소지할 수 있는 탄약 수 
    [SerializeField] int maxCurrentBullet; //내가 최대로 장전할 수 있는 탄약 수 
    [SerializeField] float rateTime;  //연사속도
    [SerializeField] int power;       //데미지

    private int haveBullet; //내가 소지하고 있는 탄약 수 
    private int currentBullet; //내가 장전하고 있는 탄약 수 
    

    public bool IsEmptyCurrent => currentBullet <= 0; //현재 장전되어 있는 탄약이 없음 
    public bool IsAmmoOut => haveBullet <= 0; //가지고 있는 탄약이 없음 

    public int HaveBullet => haveBullet;
    public int CurrentBullet => currentBullet;
    public float RateTime => rateTime;
    public int Power => power;

    public void Init()
    {
        haveBullet = maxHaveBullet;
        currentBullet = maxCurrentBullet;
    }

    public void Shoot()
    {
        currentBullet -= 1;
    }

    public void Reload()
    {
        int need = maxCurrentBullet - currentBullet; //장전에 필요한 탄약 수 
        if(haveBullet < need) //내가 필요한 양보다 소지량이 적을 경우 
        {
            currentBullet += haveBullet; //남은 소지량 만큼 현재 탄약을 더하고 
            haveBullet = 0; //소지량은 0이 됨 
        }
        else //필요한 양만큼 충분히 소지하고 있을 경우 
        {
            currentBullet = maxCurrentBullet; //최대로 충전 
            haveBullet -= need; //소지량에서 필요량만큼 감소
        }
    }
}

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] Animator anim;
    [SerializeField] Weapon weapon;

    public int MaxBullet => (weapon != null) ? weapon.HaveBullet : 0;
    public int CurrentBullet => (weapon != null) ? weapon.CurrentBullet : 0;
    float nextFireTime; //다음 총을 쏠 수 있는 시간
    bool isReload; //장전중인가?

    private void Start()
    {
        weapon.Init();
    }

    private void Update()
    {
        Fire();
        Reload();
    }

    private void Fire()
    {
        if(isReload)
        {
            return;
        }

        //Time.time : 게임이 시작하고 몇초가 흘렀는가
        if (Input.GetMouseButton(0) && !weapon.IsEmptyCurrent && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + weapon.RateTime;
            weapon.Shoot();
            anim.SetTrigger("onFire");
            AudioManager.Instance.PlaySE("shoot", 0.1f);
        }
    }

    private void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReload && !weapon.IsAmmoOut)
        {
            isReload = true;
            anim.SetTrigger("onReload");
            AudioManager.Instance.PlaySE("reload");
        }
    }

    private void OnEndReload()
    {
        isReload = false;
        weapon.Reload();
    }
}
