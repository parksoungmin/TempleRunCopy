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
       QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 999;
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
                gameOver = false;
                currentGameOverTime = 0f;
                scoreUi.SetActive(true);
                inGameUi.SetActive(false);
            }
        }
    }
    public void GameOver()
    {
        if (player.protect.gameObject.activeSelf)
        {
            player.protect.DestroyProtect();
        }
        else if (player.invincibility.gameObject.activeSelf)
        {

        }
        else
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
}
