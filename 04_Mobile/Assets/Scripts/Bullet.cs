using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float moveSpeed;

    private void Update()
    {
        //내 정면으로 moveSpeed로 날아가라 
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public void Shoot(Vector3 dir, float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
        transform.rotation = Quaternion.LookRotation(dir); //해당 방향 바라봄
    }
}
