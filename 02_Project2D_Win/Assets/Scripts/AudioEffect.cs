using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    [SerializeField] AudioSource source;

    //델리게이트 : 함수를 가지는 변수
    //접근제한자 delegate 반환형 델리게이트명(매개변수)
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
        while(source.isPlaying) //만약 플레이 중이라면
        {
            yield return null; //1프레임 대기
        }
        onReturn?.Invoke(this); //등록된 이벤트를 통해 반환
    }


}
