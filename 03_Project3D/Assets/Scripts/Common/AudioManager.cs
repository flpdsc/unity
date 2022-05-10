using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : ObjectPool<AudioManager, AudioEffect>
{
    [SerializeField] AudioClip[] effects;     
    AudioSource audioSource;
   
    void Start()
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
    public void PlaySE(string name, float volume = 1.0f)
    {
        for(int i = 0; i< effects.Length; i++)
        {
            
            if(effects[i].name == name)
            {
                AudioClip clip = effects[i];                    
                AudioEffect effect = GetPool();
                effect.PlaySE(clip, volume);                          
                break;
            }
        }
    }

}
