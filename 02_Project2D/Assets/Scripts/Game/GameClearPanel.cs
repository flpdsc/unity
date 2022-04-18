using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClearPanel : MonoBehaviour
{
    [SerializeField] Text eatText;
    [SerializeField] Text coinText;
    [SerializeField] Image[] starImages;

    private void OnEnable()
    {
        eatText.text = "0";
        coinText.text = "0";
        for (int i = 0; i < starImages.Length; ++i)
            starImages[i].gameObject.SetActive(false);

        StartCoroutine(ShowResult());
    }

    IEnumerator ShowResult()
    {
        yield return new WaitForSeconds(0.2f);
        eatText.text = Player.Instance.Coin.ToString("#,##0");

        yield return new WaitForSeconds(0.2f);
        coinText.text = GameManager.Instance.coin.ToString("#,##0");

        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < starImages.Length; ++i)
            yield return StartCoroutine(ShowStar(starImages[i].transform));
    }

    IEnumerator ShowStar(Transform star)
    {
        float scale = 2.0f;
        float speed = 3.0f;
        star.gameObject.SetActive(true);
        star.localScale = new Vector3(scale, scale, 1.0f);

        while(scale>1.0f)
        {
            scale = Mathf.Clamp(scale - (Time.deltaTime * speed), 1.0f, float.MaxValue);
            star.localScale = new Vector3(scale, scale, 1.0f);
            yield return null;
        }
    }
}
