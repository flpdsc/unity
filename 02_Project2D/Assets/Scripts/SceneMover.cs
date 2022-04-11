using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //씬 관리 네임스페이스 

public class SceneMover : MonoBehaviour
{

    [SerializeField] Image blindImage;
    [SerializeField] float fadeTime = 2f;

    public static bool isFading
    {
        get;
        private set;
    }

    //bool isFading; //화면 전환 중인가?
    //public bool IsFading => isFading;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void NextScene(string sceneName)
    {
        if (isFading) return;
        StartCoroutine(FadeOut(sceneName));
    }

    /*
    [ContextMenu("FadeOut")] public void OnFaceOut()
    {
        if (isFading) return;   
        StartCoroutine(FadeOut()); //FadeOut 코루틴 실행
    }

    [ContextMenu("FadeIn")] public void OnFaceIn()
    {
        if (isFading) return;
        StartCoroutine(FadeIn()); //FadeIn 코루틴 실행
    }
    */

    IEnumerator FadeOut(string sceneName)
    {
        isFading = true;
        //n초동안 blindImage의 a값이 0에서 1로 변경됨
        float time = 0f; //타이머
        float MAX_TIME = fadeTime; //화면 전환 시간
        Color color = blindImage.color; //원색상

        blindImage.enabled = true; //blindImage 활성화 

        do
        {
            //Mathf.Clamp(값, 최소값, 최대값) : 값을 최소, 최대에 고정시킨 후 반환
            time = Mathf.Clamp(time + Time.deltaTime, 0f, MAX_TIME);

            color.a = time / MAX_TIME; //시간의 흐름에 따른 비율 계산 
            blindImage.color = color; //blindImage에 색상 대입
            yield return null; //1프레임 쉼 
        }
        while (time < MAX_TIME);

        isFading = false;
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        //n초동안 blindImage의 a값이 1에서 0으로 변경됨
        float time = fadeTime; //타이머
        float MAX_TIME = fadeTime; //화면 전환 시간
        Color color = blindImage.color; //원색상

        blindImage.enabled = true; //blindImage 활성화 

        do
        {
            //Mathf.Clamp(값, 최소값, 최대값) : 값을 최소, 최대에 고정시킨 후 반환
            time = Mathf.Clamp(time - Time.deltaTime, 0f, MAX_TIME);

            color.a = time / MAX_TIME; //시간의 흐름에 따른 비율 계산 
            blindImage.color = color; //blindImage에 색상 대입
            yield return null; //1프레임 쉼 
        }
        while (time > 0f);

        blindImage.enabled = false; //blindImage 활성화 
        isFading = false;
    }


}
