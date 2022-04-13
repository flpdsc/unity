using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    [SerializeField] AudioSource source;

    //��������Ʈ : �Լ��� ������ ����
    //���������� delegate ��ȯ�� ��������Ʈ��(�Ű�����)
    public delegate void ReturnPoolEvent(AudioEffect se);
    public event ReturnPoolEvent onReturn;

    public void PlaySE(AudioClip clip)
    {
        source.clip = clip;
        source.loop = false;
        source.Play();

        StartCoroutine(CheckPlay());
    }

    IEnumerator CheckPlay()
    {
        while(source.isPlaying) //���� �÷��� ���̶��
        {
            yield return null; //1������ ���
        }
        onReturn?.Invoke(this); //��ϵ� �̺�Ʈ�� ���� ��ȯ
    }


}
