using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] float leftDistance;
    [SerializeField] float rightDistance;
    [SerializeField] float moveSpeed;
    [SerializeField] bool isRight;

    Vector3 originPos;
    Vector3 leftPos;
    Vector3 rightPos;

    Transform player;

    private void Start()
    {
        originPos = transform.position;
        leftPos = transform.position - new Vector3(leftDistance, 0, 0);
        rightPos = transform.position + new Vector3(rightDistance, 0, 0);
    }

    private void Update()
    {
        Vector3 destination = isRight ? rightPos : leftPos;
        float movement = moveSpeed * Time.deltaTime * (isRight ? 1f : -1f);
        transform.position += new Vector3(movement, 0f, 0f);

        //플레이어가 내 위에 있다면
        if(player!=null)
        {
            player.position += new Vector3(movement, 0f, 0f);
        }

        if(Vector3.Distance(transform.position, destination) < Mathf.Abs(movement))
        {
            isRight = !isRight;
        }
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
        if(Application.isPlaying==false)
        {
            leftPos = transform.position - new Vector3(leftDistance, 0, 0);
            rightPos = transform.position + new Vector3(rightDistance, 0, 0);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(leftPos, 0.1f);
        Gizmos.DrawWireSphere(rightPos, 0.1f);
    }
}
