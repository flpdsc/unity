using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] GameObject holePrefab;

    Rigidbody rigid;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hole = Instantiate(holePrefab);
        hole.transform.position = transform.position;
        hole.transform.rotation = Quaternion.LookRotation(collision.contacts[0].normal);
        Instantiate(holePrefab, transform.position, transform.rotation);
        Destroy(gameObject);                
    }

    public void Shoot(float bulletSpeed)
    {
        rigid = GetComponent<Rigidbody>();
        //velocity(속력) : Vector3
        //= Vector3.forward(월드상 정면) * 속도 = 벡터 
        rigid.velocity = transform.forward * bulletSpeed;

    }
}
