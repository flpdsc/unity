using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    [SerializeField] AudioSource source;

    public void PlaySE(AudioClip clip)
    {
        source.clip = clip;
        source.loop = false;
        source.Play();
    }

    private void Update()
    {
        if(!source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
