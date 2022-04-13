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
    [SerializeField] Transform storageParent; //����� �θ� ������Ʈ
    [SerializeField] int poolCount = 5; //�̸� � ����� ������
    [SerializeField] AudioClip[] effects; //ȿ���� �迭

    AudioSource audioSorce;
    Stack<AudioEffect> storage; //�̸� ������ ȿ������ �����ϴ� ����

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSorce = GetComponent<AudioSource>();
        storage = new Stack<AudioEffect>(); //AudioEffect�� Stack��ü ����
        //�̸� poolCount��ŭ ��ü �߰�
        for (int i = 0; i < poolCount; ++i)
        {
            CreatePool();
        }
    }

    private void CreatePool()
    {
        AudioEffect newPool = Instantiate(sePrefab); //sePrefab����
        newPool.transform.SetParent(storageParent); //����ǰ�� �θ� storageParent�� ����
        newPool.Setup(ReturnPool); //Setup�� ���� �̺�Ʈ ���
        storage.Push(newPool); //storage�� �߰�
    }

    private AudioEffect GetPool()
    {
        if (storage.Count <= 0) //���� ���� ī��Ʈ�� 0 �����̸� ���� �������
        {
            CreatePool();
        }
        AudioEffect pool = storage.Pop(); //storage���� �ϳ� ����
        pool.transform.SetParent(transform); //�θ� ������Ʈ�� transfom(��, AudioManager)�� ����
        return pool;
    }

    private void ReturnPool(AudioEffect se)
    {
        se.transform.SetParent(storageParent); //�θ� ������Ʈ�� storageParent�� ����
        storage.Push(se); //����ҿ� Ǫ��
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