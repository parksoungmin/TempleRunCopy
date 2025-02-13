using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI gameOverDistanceText;
    public TextMeshProUGUI gameOverBestDistance;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverCoinText;

    public TextMeshProUGUI inGameScoreText;
    public TextMeshProUGUI inGameCoinText;

    public TextMeshProUGUI pauseScoreText;
    public TextMeshProUGUI pauseCoinText;
    public TextMeshProUGUI pauseDistanceText;

    public GameObject upgradeUi;
    public GameObject inGameUi;
    public GameObject GameOverUi;
    public GameObject mainMenuUi;
    public GameObject pauseUi;
    public GameObject optionUi;

    public int score = 0;
    public int currentGameacquireCoin;
    public float distanceBestRecord;
    private int totalDistance;
    private void Awake()
    {
        ChackSceneChange();
    }
    private void Start()
    {
        distanceBestRecord = GameData.distanceBestRecord;
    }

    public void OnButtonClickGameReStart()
    {
    }
    private void Update()
    {
        score = Mathf.RoundToInt(player.totalDistance * 10f);
        totalDistance = Mathf.RoundToInt(player.totalDistance);
        inGameScoreText.text = $"{score}";
    }
    public void DistanceSet()
    {
        if (distanceBestRecord < player.totalDistance)
        {
            distanceBestRecord = player.totalDistance;
        }
        gameOverDistanceText.text = $"Distance       {totalDistance}";
    }
    public void BastDistanceSet()
    {
        gameOverBestDistance.text = $"Best Distance    {Mathf.RoundToInt(distanceBestRecord)}";
    }
    public void GameOverUISet()
    {
        gameOverCoinText.text = $"Coin              {currentGameacquireCoin}";
        gameOverScoreText.text = $"{score}";
    }
    public void AddCoin(int coin)
    {
        currentGameacquireCoin += coin;
        inGameCoinText.text = $"{this.currentGameacquireCoin}";
    }
    public void UpgradeUiOn()
    {
        upgradeUi.SetActive(true);
    }
    public void GameOver()
    {
        DistanceSet();
        GameOverUISet();
        BastDistanceSet();
    }
    public void OnClickChangeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void InGameUiSet()
    {
        Time.timeScale = 2f;
        mainMenuUi.SetActive(false);
        upgradeUi.SetActive(false);
        inGameUi.SetActive(true);
        pauseUi.SetActive(false);
        GameOverUi.SetActive(false);
        GameData.ingameUiOn = true;
        GameData.mainMenuUiOn = false;
        GameData.gameOverUiOn = false;
        GameData.upGradeUiOn = false;
    }
    public void OnGameUiSet()
    {
        Time.timeScale = 0f;
        mainMenuUi.SetActive(true);
        upgradeUi.SetActive(false);
        inGameUi.SetActive(false);
        pauseUi.SetActive(false);
        GameOverUi.SetActive(false);
        GameData.ingameUiOn = false;
        GameData.mainMenuUiOn = true;
        GameData.gameOverUiOn = false;
        GameData.upGradeUiOn = false;
    }
    public void OnGameOverUiSet()
    {
        Time.timeScale = 0f;
        GameOver();
        mainMenuUi.SetActive(false);
        upgradeUi.SetActive(false);
        inGameUi.SetActive(false);
        GameOverUi.SetActive(true);
        pauseUi.SetActive(false);
        GameData.ingameUiOn = false;
        GameData.mainMenuUiOn = false;
        GameData.gameOverUiOn = true;
        GameData.upGradeUiOn = false;
    }
    public void OnUpgradeUiSet()
    {
        Time.timeScale = 0f;
        mainMenuUi.SetActive(false);
        upgradeUi.SetActive(true);
        inGameUi.SetActive(false);
        GameOverUi.SetActive(false);
        pauseUi.SetActive(false);
        GameData.ingameUiOn = false;
        GameData.mainMenuUiOn = false;
        GameData.gameOverUiOn = false;
        GameData.upGradeUiOn = true;
    }
    public void OnPauseUiSet()
    {
        Time.timeScale = 0f;
        pauseUi.SetActive(true);
        pauseCoinText.text = $"{this.currentGameacquireCoin}";
        pauseDistanceText.text = $"{totalDistance}M";
        pauseScoreText.text = $"{score}";
    }
    public void OffPauseUiSet()
    {
        pauseUi.SetActive(false);
        Time.timeScale = 2f;
    }
    public void OnOptionUiSet()
    {
        optionUi.SetActive(true);
    }
    public void OffOptionUiSet()
    {
        optionUi.SetActive(false);
    }
private void ChackSceneChange()
    {
        if(GameData.ingameUiOn)
        {
            InGameUiSet();
        } 
        if(GameData.mainMenuUiOn)
        {
            OnGameUiSet();
        }
        if(GameData.gameOverUiOn)
        {
            OnGameOverUiSet();
        }
        if(GameData.upGradeUiOn)
        {
            OnUpgradeUiSet();
        }
    }
}
