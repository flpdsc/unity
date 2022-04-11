using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMover : MonoBehaviour
{
    [SerializeField] Image blindImage;
    [SerializeField] float fadeTime = 2f;

    public static bool isFading
    {
        get;
        private set;
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void NextScene(string sceneName)
    {
        if (isFading) return;
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        float time = fadeTime;
        Color color = blindImage.color;

        blindImage.enabled = true;

        do
        {
            time = Mathf.Clamp(time - Time.deltaTime, 0f, fadeTime);
            color.a = time / fadeTime;
            blindImage.color = color;
            yield return null;
        }
        while (time > 0f);

        blindImage.enabled = false;
        isFading = false;
    }

    IEnumerator FadeOut(string sceneName)
    {
        isFading = true;
        float time = 0f;
        Color color = blindImage.color;

        blindImage.enabled = true;

        do
        {
            time = Mathf.Clamp(time + Time.deltaTime, 0f, fadeTime);
            color.a = time / fadeTime;
            blindImage.color = color;
            yield return null;
        }
        while (time < fadeTime);

        SceneManager.LoadScene(sceneName);
    }

}
