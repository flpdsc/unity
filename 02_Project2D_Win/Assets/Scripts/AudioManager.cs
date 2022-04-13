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
    [SerializeField] Transform storageParent; //저장소 부모 오브젝트
    [SerializeField] int poolCount = 5; //미리 몇개 만들어 놓을지
    [SerializeField] AudioClip[] effects; //효과음 배열

    AudioSource audioSorce;
    Stack<AudioEffect> storage; //미리 만들어둔 효과음을 저장하는 변수

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSorce = GetComponent<AudioSource>();
        storage = new Stack<AudioEffect>(); //AudioEffect형 Stack객체 생성
        //미리 poolCount만큼 객체 추가
        for (int i = 0; i < poolCount; ++i)
        {
            CreatePool();
        }
    }

    private void CreatePool()
    {
        AudioEffect newPool = Instantiate(sePrefab); //sePrefab복제
        newPool.transform.SetParent(storageParent); //복제품의 부모를 storageParent로 설정
        newPool.Setup(ReturnPool); //Setup을 통해 이벤트 등록
        storage.Push(newPool); //storage에 추가
    }

    private AudioEffect GetPool()
    {
        if (storage.Count <= 0) //만약 남은 카운트가 0 이하이면 새로 만들어줌
        {
            CreatePool();
        }
        AudioEffect pool = storage.Pop(); //storage에서 하나 꺼냄
        pool.transform.SetParent(transform); //부모 오브젝트를 transfom(나, AudioManager)로 변경
        return pool;
    }

    private void ReturnPool(AudioEffect se)
    {
        se.transform.SetParent(storageParent); //부모 오브젝트를 storageParent로 변경
        storage.Push(se); //저장소에 푸시
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