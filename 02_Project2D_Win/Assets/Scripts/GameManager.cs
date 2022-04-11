using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject pausePanel;

    bool isGameOver = false;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void Update()
    {
        if (!isGameOver && player.isDead)
        {
            StartCoroutine(GameOver());
        }

        if (!isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !SceneMover.isFading)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f; //월드 전체 시간 배율 x0배로 설정
            }
        }
    }

    public void OnReleasePause()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnExitGameScene()
    {
        Time.timeScale = 1f;
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
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