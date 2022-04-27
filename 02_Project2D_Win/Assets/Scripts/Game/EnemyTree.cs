using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTree : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] Transform bulletPivot;
    [SerializeField] float attackDelay = 2;
    [SerializeField] int hp = 2;

    Animator anim;
    float attackTime = 0f;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        attackTime += Time.deltaTime;
        if(attackTime>attackDelay)
        {
            anim.SetTrigger("onAttack");
            attackTime = 0f;
        }
    }

    void Fire()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletPivot.position, bulletPivot.rotation);
        bullet.name = "Tree_Bullet";

        bullet.Shoot(Vector3.left);
    }

    public void OnDamaged()
    {
        hp -= 1;
        if(hp<=0)
        {
            Destroy(gameObject);
        }
        else
        {
            anim.SetTrigger("onDamaged");
        }
    }
}