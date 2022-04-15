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
        StartCoroutine(GameStart());
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
                Time.timeScale = 0f; //���� ��ü �ð� ���� x0��� ����
            }
        }

        //�׽�Ʈ
        if (Input.GetKeyDown(KeyCode.Q))
            AudioManager.Instance.PlaySE("jump");
        if (Input.GetKeyDown(KeyCode.W))
            AudioManager.Instance.PlaySE("light");
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

    public void OnGameClear()
    {
        StartCoroutine(GameClear());
    }

    private IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlayBGM(); //�̱����� �̿��� AudioManager ��ü�� ����
    }

    private IEnumerator GameOver()
    {
        isGameOver = true;

        yield return new WaitForSeconds(1.5f);

        //GameObject.SetActive(bool) : void
        //���� ������Ʈ ��ü�� Ȱ��/��Ȱ��ȭ ��
        gameOverPanel.SetActive(true);

        AudioManager.Instance.StopBGM();

        yield return new WaitForSeconds(4.0f);

        //Game�� �ε�
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    private IEnumerator GameClear()
    {
        player.OnSwitchLockControl(true);
        AudioManager.Instance.StopBGM();
        yield return null;
    }
}