using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI gameOverDistanceText;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI gameOverCoinText;
    public TextMeshProUGUI inGameScoreText;
    public TextMeshProUGUI inGameCoinText;

    public int score = 0;
    public int coin = 0;
    public void OnButtonClickGameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        score = Mathf.RoundToInt(player.totalDistance * 10f);
        inGameScoreText.text = $"{score}M";
    }
    public void DistanceSet()
    {
        var totalDistance = Mathf.RoundToInt(player.totalDistance);
        gameOverDistanceText.text = $"Distance       {totalDistance}";
    }
    public void AddCoin(int coin)
    {
        this.coin += coin;
        inGameCoinText.text = $"{this.coin}";
    }
    public void GameOverUISet()
    {
        gameOverCoinText.text = $"Coin              {coin}";
        gameOverScoreText.text = $"{score}";
    }
}
