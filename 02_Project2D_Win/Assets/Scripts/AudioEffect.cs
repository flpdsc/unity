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
        while(source.isPlaying) //만약 플레이 중이라면
        {
            yield return null; //1프레임 대기
        }
        onReturn?.Invoke(this); //등록된 이벤트를 통해 반환
    }

    public void Setup(ReturnPoolEvent<AudioEffect> onReturn)
    {
        this.onReturn = onReturn;
    }


}
