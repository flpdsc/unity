using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    Rigidbody rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal"); //오른쪽 : 1, 왼쪽 : -1
        float z = Input.GetAxisRaw("Vertical"); //위쪽 : 1, 아래쪽 -1

        //Vector3.right : 월드 좌표 기준으로 오른쪽 방향값
        //transform.right : 나를 기준으로 오른쪽 방향값

        //direction : 내 키 입력에 따라 가고자 하는 방향
        //movement : 이동량 
        //방향에 -1을 곱하면 반대가 됨
        Vector3 direction = (transform.right * x) + (transform.forward * z);
        Vector3 velocity = direction * moveSpeed;

        velocity.y = rigid.velocity.y;

        //transform.position += movement;
        rigid.velocity = velocity;
    }
}
