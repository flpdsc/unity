using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour, IObjectPool<AudioEffect>
{
    [SerializeField] AudioSource source;

    ReturnPoolEvent<AudioEffect> onReturn;

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

    public void Setup(ReturnPoolEvent<AudioEffect> onReturn)
    {
        this.onReturn = onReturn;
    }


}
