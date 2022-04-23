using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] Vector2[] destinations;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isReverse;

    Vector3 originPos;
    Vector3 beforePostion;

    Transform player;
    int index;

    private void Start()
    {
        originPos = transform.position;
        transform.position = GetDestination(index);
    }

    private void Update()
    {
        Vector3 destination = GetDestination(index);

        Vector3 beforePos = transform.position;

        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);


        //플레이어가 내 위에 있다면
        if(player!=null)
        {
            Vector3 movement = transform.position - beforePos;
            player.position += movement;
        }

        if(transform.position == destination)
        {
            if(!isReverse && index == destinations.Length-1)
            {
                isReverse = true;
            }
            else if(isReverse && index==0)
            {
                isReverse = false;
            }
            index += isReverse ? -1 : 1;
        }
    }

    private Vector3 GetDestination(int index)
    {
        Vector3 position = destinations[index];
        Vector3 destination = originPos + position;
        return destination;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == Player.Instance.gameObject)
        {
            player = Player.Instance.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject == Player.Instance.gameObject)
        {
            player = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(destinations == null)
        {
            return;
        }

        if(!Application.isPlaying)
        {
            originPos = transform.position;
        }

        Gizmos.color = Color.red;
        
        for(int i=0; i<destinations.Length; ++i)
        {
            Vector3 pos = destinations[i];
            Gizmos.DrawSphere(originPos + pos, 0.1f);
        }
    }
}
