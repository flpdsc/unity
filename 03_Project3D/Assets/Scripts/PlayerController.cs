using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class Weapon
{
    public int maxBullet;
    public int currentBullet;
    public float rateTime;  //연사속도
    public int power;       //데미지


    public bool isEmpty => currentBullet <= 0;

    public Weapon(int maxBullet)
    {
        this.maxBullet = maxBullet;
        currentBullet = maxBullet;
    }


}

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] Animator anim;
    [SerializeField] Weapon weapon;

    public int MaxBullet => (weapon != null) ? weapon.maxBullet : 0;
    public int CurrentBullet => (weapon != null) ? weapon.currentBullet : 0;
    float nextFireTime; //다음 총을 쏠 수 있는 시간

    private void Update()
    {
        //Time.time : 게임이 시작하고 몇초가 흘렀는가
        if(Input.GetMouseButton(0) && !weapon.isEmpty && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + weapon.rateTime;
            weapon.currentBullet -= 1;
            anim.SetTrigger("onFire");
            AudioManager.Instance.PlaySE("shoot");
        }
    }
}
