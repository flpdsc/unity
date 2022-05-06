using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                       // UI ���� ���ӽ����̽�.
using UnityEngine.SceneManagement;          // �� ���� ���ӽ����̽�.

public class SceneMover : MonoBehaviour
{
    [SerializeField] Image blindImage;
    [SerializeField] float fadeTime;

    public static bool isFading
    {
        get;
        private set;
    }

    /*
    bool isFading;                          // ȭ�� ��ȯ���ΰ�?
    public bool IsFading => isFading;       // ������Ƽ�� �ܺ� ����. (readonly)
    */

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    /*
    [ContextMenu("FadeOut")]                // Component�� ������ Ŭ������ �� ������ �޴�.
    public void OnFadeOut()
    {
        if (isFading)
            return;

        StartCoroutine(FadeOut());          // FadeOut �ڷ�ƾ ����.
    }
    [ContextMenu("FadeIn")] 
    public void OnFadeIn()
    {
        if (isFading)
            return;

        StartCoroutine(FadeIn());
    }
    */

    public void NextScene(string sceneName)
    {
        if (isFading)
            return;

        StartCoroutine(FadeOut(sceneName));
    }
    IEnumerator FadeOut(string sceneName)
    {
        // n(fadeTime)�� ���� blindImage�� a���� 0���� 1�� ����ȴ�.
        isFading = true;

        float MAX_TIME = fadeTime;          // ȭ�� ��ȯ �ð�.
        float time = 0f;                    // Ÿ�̸�.
        Color color = blindImage.color;     // ������.

        blindImage.enabled = true;          // blindImage�� Ȱ��ȭ�Ѵ�.

        do
        {
            //Mathf.Clmap(��, �ּҰ�, �ִ밪) : ���� �ּ�, �ִ뿡 ������Ų �� ��ȯ.
            time = Mathf.Clamp(time + Time.deltaTime, 0.0f, MAX_TIME);

            color.a = time / MAX_TIME;      // �ð��� �帧�� ���� ���� ���.
            blindImage.color = color;       // blindImage�� ���� ����.
            yield return null;              // 1������ ����(�� �ϴ� ��������)
        }
        while (time < MAX_TIME);            // time���� maxTime���� ������ �ٽ� do�� ����.
        
        SceneManager.LoadScene(sceneName);
    }
    IEnumerator FadeIn()
    {
        // n(fadeTime)�� ���� blindImage�� a���� 1���� 0�� ����ȴ�.
        isFading = true;

        float MAX_TIME = fadeTime;          // ȭ�� ��ȯ �ð�.
        float time = fadeTime;              // Ÿ�̸�.
        Color color = blindImage.color;     // ������.

        blindImage.enabled = true;          // blindImage�� Ȱ��ȭ�Ѵ�.

        do
        {
            //Mathf.Clmap(��, �ּҰ�, �ִ밪) : ���� �ּ�, �ִ뿡 ������Ų �� ��ȯ.
            time = Mathf.Clamp(time - Time.deltaTime, 0.0f, MAX_TIME);

            color.a = time / MAX_TIME;      // �ð��� �帧�� ���� ���� ���.
            blindImage.color = color;       // blindImage�� ���� ����.
            yield return null;              // 1������ ����(�� �ϴ� ��������)
        }
        while (time > 0.0f);                // time���� maxTime���� ������ �ٽ� do�� ����.

        blindImage.enabled = false;         // blindImage�� ��Ȱ��ȭ�Ѵ�.
        isFading = false;                   // ȭ�� ��ȯ ���� false.
    }
}
