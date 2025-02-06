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
    public GameObject upgradeUi;
    public int score = 0;
    public int currentGameacquireCoin;
    public float distanceBestRecord;
    private void Start()
    {
        distanceBestRecord = GameData.distanceBestRecord;
    }

    public void OnButtonClickGameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        score = Mathf.RoundToInt(player.totalDistance * 10f);
        inGameScoreText.text = $"{score}";
    }
    public void DistanceSet()
    {
        if(distanceBestRecord < player.totalDistance)
        {
            distanceBestRecord = player.totalDistance;
        }
        var totalDistance = Mathf.RoundToInt(player.totalDistance);
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
}
