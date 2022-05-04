using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSFX : StateMachineBehaviour
{
    [SerializeField] AudioClip sfx;

    AudioSource sfxSource;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (sfxSource == null)
            sfxSource = animator.GetComponent<AudioSource>();

        sfxSource.clip = sfx;
        sfxSource.Play();
    }
}
