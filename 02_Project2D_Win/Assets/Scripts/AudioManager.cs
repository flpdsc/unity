using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Singleton
    //어디에서든 해당 객체에 접근할 수 있는 디자인 패턴 중 하나
    //단, 해당 객체는 하나만 존재해야 함
    static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] AudioEffect sePrefab; //효과음 스피커
    [SerializeField] AudioClip[] effects; //효과음 배열

    AudioSource audioSorce;

    private void Awake()
    {
        instance = this;
    }

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
                AudioEffect effect = Instantiate(sePrefab); //prefab의 복제본 생성
                effect.PlaySE(clip); //복제본 재생
                break;
            }
        }
    }
}