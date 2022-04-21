using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] float leftDistance; //왼쪽으로 이동할 거리 
    [SerializeField] float rightDistance; //오른쪽으로 이동할 거리 
    [SerializeField] float moveSpeed;
    [SerializeField] bool isRight;

    Vector3 originPos; //최초 위치 
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
        Vector3 destination = isRight ? rightPos : leftPos; //목적지 
        float movement = moveSpeed * Time.deltaTime * (isRight ? 1f : -1f); //이동량 
        transform.position += new Vector3(movement, 0f, 0f); //이동량만큼 위치 조정 

        //플레이어가 내 위에 있다면 
        if(player!=null)
        {
            player.position += new Vector3(movement, 0f, 0f);
        }

        if(Vector3.Distance(transform.position, destination) < Mathf.Abs(movement)) //목적지와의 거리가 짧을 때 
        {
            isRight = !isRight; //bool값 반전  
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //충돌한 물체가 player와 같다면 
        if(collision.gameObject == Player.Instance.gameObject)
        {
            player = Player.Instance.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //충돌을 벗어난 물체가 player라면
        if(collision.gameObject == Player.Instance.gameObject)
        {
            player = null;
        }
        
    }

    //Gizmo 그리기
    // 내가 이동할 왼쪽 오른쪽 위치를 그림 
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
