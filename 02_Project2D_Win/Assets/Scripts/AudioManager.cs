using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Singleton
    //��𿡼��� �ش� ��ü�� ������ �� �ִ� ������ ���� �� �ϳ�
    //��, �ش� ��ü�� �ϳ��� �����ؾ� ��
    static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] AudioEffect sePrefab; //ȿ���� ����Ŀ
    [SerializeField] AudioClip[] effects; //ȿ���� �迭

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
                AudioClip clip = effects[i]; //effects�� i��° ����
                AudioEffect effect = Instantiate(sePrefab); //prefab�� ������ ����
                effect.PlaySE(clip); //������ ���
                break;
            }
        }
    }
}