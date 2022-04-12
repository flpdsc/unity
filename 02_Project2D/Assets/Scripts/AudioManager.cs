using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Singleton
    static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] AudioEffect sePrefab;
    [SerializeField] AudioClip[] effects;

    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

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
                AudioEffect effect = Instantiate(sePrefab);
                effect.PlaySE(clip);
                break;
            }
        }
    }
}
