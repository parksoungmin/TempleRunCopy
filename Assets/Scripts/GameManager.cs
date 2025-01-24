using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject scoreUi;
    public GameObject inGameUi;
    public Enemy enemy;

    private float currentGameOverTime = 0f;
    public float gameOverTime = 2f;

    public bool gameOver = false;
    public void Start()
    {
        scoreUi.SetActive(false);
        inGameUi.SetActive(true);
    }
    public void Update()
    {
        if (gameOver)
        {
            currentGameOverTime += Time.deltaTime;
            if (currentGameOverTime > gameOverTime)
            {
                currentGameOverTime = 0f;
                scoreUi.SetActive(true);
                inGameUi.SetActive(false);
            }
        }
    }
    public void GameOver()
    {
        UiManager uiManager = inGameUi.GetComponent<UiManager>();
        uiManager.DistanceSet();
        uiManager.GameOverUISet();
        enemy.MoveToPlayer(player.transform.position);
        gameOver = true;
        player.speed = 0f;
        player.tiltSpeed = 0f;
    }
}
