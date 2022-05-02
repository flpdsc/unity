using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdle : StateMachineBehaviour
{
    [SerializeField] int maxIdleIndex;

    [Range(0f, 100f)]
    [SerializeField] float motionPersent;

    const string KEY_IDLE = "idle";

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int idleIndex = 0;
        if(Random.value * 100f < motionPersent)
        {
            idleIndex = Random.Range(0, maxIdleIndex);
        }
        animator.SetInteger(KEY_IDLE, idleIndex);
    }
}
