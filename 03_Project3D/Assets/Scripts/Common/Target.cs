using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] HpBar hpBar;
    [SerializeField] float maxHP;
    [SerializeField] float hp;

    private void Start()
    {
        hpBar.OnUpdateHp(hp, maxHP);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(hp>0)
        {
            hp -= 1;
            hpBar.OnUpdateHp(hp, maxHP);
        }
        
    }
}
