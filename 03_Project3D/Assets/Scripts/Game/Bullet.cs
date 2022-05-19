using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject holePrefab;

    Rigidbody rigid;

    private void OnCollisionEnter(Collision collision)
    {
        //충돌한 충돌체에게서 Damageable 컴포넌트 검색 
        Damageable target = collision.gameObject.GetComponent<Damageable>();
        if(target != null)
        {
            target.OnDamaged(10);
            DamageManager.Instance.ShowDamageText(transform.position, 10);
        }
        else
        {
            GameObject hole = Instantiate(holePrefab);
            hole.transform.position = transform.position;
            hole.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        }
        Destroy(gameObject);                
    }

    public void Shoot(float bulletSpeed, Vector3 direction)
    {
        rigid = GetComponent<Rigidbody>();
        //velocity(속력) : Vector3
        //= Vector3.forward(월드상 정면) * 속도 = 벡터 
        rigid.velocity = direction * bulletSpeed;

    }
}
