using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDown : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();   //�浹�� �浹ü���Լ� Player �˻�
        if(player != null)  //�÷��̾ �˻��ߴٸ�
        {
            player.OnFallDown(); //�������ٰ� ����
        }
    }
}
