using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] Vector2[] destinations; //이동해야 할 위치 배열
    [SerializeField] float moveSpeed;
    [SerializeField] bool isReverse;

    Vector3 originPos; //최초 위치
    Vector3 beforePosition; //이전 위치

    Transform player; //플레이어 
    int index; //목적지 인덱스
               
    private void Start()
    {
        originPos = transform.position;
        transform.position = GetDestination(index);
    }

    private void Update()
    {
        Vector3 destination = GetDestination(index);

        Vector3 beforePos = transform.position;
        //MoveTowards : 현재 위치에서 특정 위치 방향으로 이동량만큼 움직였을 때의 포지션 
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        //플레이어가 내 위에 있다면 
        if(player != null)
        {
            Vector3 movement = transform.position - beforePos; //현재위치 - 이전위치 : 이동량 
            player.position += movement; //플레이어의 위치를 이동량만큼 움직임
        }

        //Mathf.Abs(value) : 값을 절대값으로 변경
        //목적지에 도착했다면 
        if(transform.position == destination)
        {
            //정방향으로 가고 있었는데 index가 마지막 위치일 경우 
            if(!isReverse && index == destinations.Length-1)
            {
                isReverse = true;
            }
            else if(isReverse && index == 0)
            {
                isReverse = false;
            }
            index += isReverse ? -1 : 1;
        }
    }

    private Vector3 GetDestination(int index)
    {
        Vector3 position = destinations[index];
        Vector3 destination = originPos + position; //목적지
        return destination;
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
    //내가 이동할 왼쪽 오른쪽 위치를 그림 
    private void OnDrawGizmosSelected()
    {
        if (destinations == null)
            return;

        if (!Application.isPlaying)
            originPos = transform.position;

        Gizmos.color = Color.red;
        for(int i=0; i< destinations.Length; ++i)
        {
            Vector3 pos = destinations[i];
            Gizmos.DrawSphere(originPos + pos, 0.1f);
        }
    }
}
