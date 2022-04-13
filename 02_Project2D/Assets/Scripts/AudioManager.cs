using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ObjectPool<AudioManager, AudioEffect>
{

    [SerializeField] AudioClip[] effects;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBGM()
    {
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    public void PlaySE(string name)
    {
        for(int i=0; i<effects.Length; ++i)
        {
            if(effects[i].name == name)
            {
                AudioClip clip = effects[i];
                AudioEffect effect = GetPool(); //효과음 오브젝트 꺼내기
                effect.PlaySE(clip); //clip 전달, 재생
                break;
            }
        }
    }
}
