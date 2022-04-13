using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Player player;

    private void LateUpdate()
    {
        if(!player.isDead)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
