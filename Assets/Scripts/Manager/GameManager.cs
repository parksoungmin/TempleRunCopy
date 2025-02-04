using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject scoreUi;
    public GameObject inGameUi;
    public Enemy enemy;
    public UpgradeUi upgradeUi;
    private float currentGameOverTime = 0f;
    public float gameOverTime = 0f;
    public UiManager uiManager;

    public int coin;

    public bool gameOver = false;

    public void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 999;
        scoreUi.SetActive(false);
        inGameUi.SetActive(true);
        uiManager = inGameUi.GetComponent<UiManager>();
        coin = GameData.coin;
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
        player.playerDead = true;
        if (player.protect.gameObject.activeSelf)
        {
            player.protect.DestroyProtect();
        }
        else if (player.invincibility.gameObject.activeSelf)
        {

        }
        else
        {
            coin += uiManager.currentGameacquireCoin;
            uiManager.GameOver();
            enemy.MoveToPlayer(player.transform.position);
            gameOver = true;
            player.transform.position = transform.position;
            player.speed = 0f;
            player.tiltSpeed = 0f;
            SaveGameProgress();
        }
    }

    public void SaveGameProgress()
    {
        SaveLoadManager.Data.magnetId = upgradeUi.magnetId;
        SaveLoadManager.Data.protectId = upgradeUi.protectId;
        SaveLoadManager.Data.coinDoubleId = upgradeUi.coinDoubleId;
        SaveLoadManager.Data.invincibilityId = upgradeUi.invincibilityId;
        SaveLoadManager.Data.coin = coin;
        SaveLoadManager.Data.distanceBestRecord = uiManager.distanceBestRecord;
        SaveLoadManager.Save();
    }
}
