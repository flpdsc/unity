using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ObjectPool<AudioManager, AudioEffect>
{
    [SerializeField] AudioClip[] effects; //ȿ���� �迭
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
                AudioClip clip = effects[i]; //effects�� i��° ����
                AudioEffect effect = GetPool();
                effect.PlaySE(clip); //������ ���
                break;
            }
        }
    }
}