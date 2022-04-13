using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ObjectPool<AudioManager, AudioEffect>
{
    [SerializeField] AudioClip[] effects; //효과음 배열
    AudioSource audioSorce;

    private void Start()
    {
        audioSorce = GetComponent<AudioSource>();
    }

    public void PlayBGM()
    {
        audioSorce.Play();
    }

    public void StopBGM()
    {
        audioSorce.Stop();
    }

    public void PlaySE(string name)
    {
        for(int i=0; i<effects.Length; ++i)
        {
            if(effects[i].name == name)
            {
                AudioClip clip = effects[i]; //effects의 i번째 대입
                AudioEffect effect = GetPool();
                effect.PlaySE(clip); //복제본 재생
                break;
            }
        }
    }
}