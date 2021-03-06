using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameClearPanel;
    [SerializeField] GameObject pausePanel;
    //[SerializeField] GameObject sceneMover;

    bool isGameOver = false;

    public int eatCount;
    public int gold;

    public int Eat => eatCount;
    public int Gold => gold;

    private void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameClearPanel.SetActive(false);
        StartCoroutine(GameStart());
    }

    void Save()
    {
        PlayerPrefs.SetInt("Eat", eatCount);
        PlayerPrefs.SetInt("Gold", gold);
    }

    void Load()
    {
        eatCount = PlayerPrefs.GetInt("Eat", 0);
        gold = PlayerPrefs.GetInt("Gold", 0);
    }

    public void AddEatCount(int amount=0)
    {
        eatCount += amount;
    }

    public void AddGold(int amount)
    {
        gold += amount;
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

        //테스트
        if (Input.GetKeyDown(KeyCode.Q))
            AudioManager.Instance.PlaySE("jump");
        if (Input.GetKeyDown(KeyCode.W))
            AudioManager.Instance.PlaySE("light");
    }

    public void OnGameClear()
    {
        Save();
        StartCoroutine(GameClear());
    }

    private IEnumerator GameStart()
    {
        Load();
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlayBGM(); //싱글톤을 이용해 AudioManager 객체에 접근
    }

    private IEnumerator GameOver()
    {
        isGameOver = true;

        yield return new WaitForSeconds(1.5f);

        //GameObject.SetActive(bool) : void
        //게임 오브젝트 자체를 활성/비활성화 함
        gameOverPanel.SetActive(true);

        AudioManager.Instance.StopBGM();

        yield return new WaitForSeconds(4.0f);

        //Game씬 로드
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    private IEnumerator GameClear()
    {
        player.OnSwitchLockControl(true); //플레이어 제어 멈추기
        AudioManager.Instance.StopBGM(); //BGM끄기

        yield return new WaitForSeconds(2f);
        gameClearPanel.SetActive(true); //클리어 패널 활성화
    }
}