using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;

    bool isGameOver = false;

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if(!isGameOver && player.isDead)
        {
            StartCoroutine(GameOver());
        }
    }

    private IEnumerator GameOver()
    {
        isGameOver = true;

        yield return new WaitForSeconds(1.5f);

        //GameObject.SetActive(bool) : void
        //게임 오브젝트 자체를 활성/비활성화 함
        gameOverPanel.SetActive(true);

        yield return new WaitForSeconds(4.0f);
        
        //Game씬 로드
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}
