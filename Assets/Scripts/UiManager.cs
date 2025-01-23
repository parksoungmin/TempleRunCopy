using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public Player player;
    public TextMeshProUGUI inGameScoreText;
    public TextMeshProUGUI gameOverDistanceText;
    public TextMeshProUGUI coinText;

    public int coin = 0;
    public void OnButtonClickGameReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        var score = Mathf.RoundToInt(player.totalDistance * 10f);
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
        coinText.text = $"{this.coin}";
    }
}
