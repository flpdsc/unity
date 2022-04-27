using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 5;
    [SerializeField] float lifeTime = 8;

    Vector3 direction;

    public void Shoot(Vector3 direction)
    {
        this.direction = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if(player!=null)
        {
            player.OnContactTrap(gameObject);
        }

        Destroy(gameObject);
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime<=0f)
        {
            Destroy(gameObject);
        }

        transform.position += direction * speed * Time.deltaTime;
    }
}
