using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Player player;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject gameClearanel;
    [SerializeField] GameObject pausePanel;
    //[SerializeField] SceneMover sceneMover;

    bool isGameOver = false;

    int eatCount;
    int gold;

    public int Eat => eatCount;
    public int Gold => gold;

    private void Start()
    {

        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        gameClearanel.SetActive(false);
        StartCoroutine(GameStart());
    }

    private void Update()
    {
        if(!isGameOver && player.isDead)
        {
            StartCoroutine(GameOver());
        }

        //게임오버 상태가 아니고 ESC키를 눌렀을 때 + 화면 전환 중이 아닐 때 
        if(!isGameOver)
        {
            if(Input.GetKeyDown(KeyCode.Escape) && !SceneMover.isFading)
            {
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }
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
        Save();
        StartCoroutine(GameClear());
    }

    private IEnumerator GameStart()
    {
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

        yield return new WaitForSeconds(4.0f);
        
        //Game씬 로드
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    private IEnumerator GameClear()
    {
        player.OnSwitchLockControl(true);
        AudioManager.Instance.StopBGM();
        yield return new WaitForSeconds(2f);
        gameClearanel.SetActive(true);
    }

    
}