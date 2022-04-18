using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearPanel : MonoBehaviour
{

    [SerializeField] Text eatText;
    [SerializeField] Text coinText;
    [SerializeField] Image[] starIamges;
    [SerializeField] Button[] buttons;

    //오브젝트가 활성화 되었을 때
    private void OnEnable()
    {
        eatText.text = "0";
        coinText.text = "0";
        for (int i = 0; i < starIamges.Length; ++i)
        {
            starIamges[i].gameObject.SetActive(false);
        }
        StartCoroutine(ShowResult());       
    }

    public void OnRetry()
    {
        Time.timeScale = 1f;
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    public void OnExitGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }


    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(0.5f);
        eatText.text = Player.Instance.Coin.ToString("#,##0");
        //eatText.text = string.Format("{0:#,##0}G", GameManager.Instance.eatCount);
        yield return new WaitForSeconds(0.5f);
        coinText.text = GameManager.Instance.coin.ToString("#,##0");
        yield return new WaitForSeconds(0.5f);
        for(int i=0; i< starIamges.Length; ++i)
        {
            yield return StartCoroutine(ShowStar(starIamges[i].transform)); //해당 코루틴이 끝날 때 까지 대기
        }
    }

    IEnumerator ShowStar(Transform star)
    {
        float scale = 2.0f;
        float speed = 1.5f;
        star.gameObject.SetActive(true);


        //scale값이 1.0보다 클 때 반복
        while(scale > 1.0f)
        {
            //scale값에 Time.deltaTime을 뺀 값이 최소 1.0f ~ 최대 MaxValue 사잇값이 되도록 고정
            scale = Mathf.Clamp(scale - Time.deltaTime * speed, 0.0f, float.MaxValue);
            star.localScale = new Vector3(scale, scale, 1.0f);
            yield return null;
        }

    }

}
