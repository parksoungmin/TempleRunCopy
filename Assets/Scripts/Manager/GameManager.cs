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

    public bool gameOver = false;
    public void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 999;
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
                uiManager.OnGameOverUiSet();
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
        else if (!player.playerDead)
        {
            enemy.MoveToPlayer(player.transform.position);
            player.animator.SetTrigger("Die");
            player.playerDead = true;
            GameData.coin += uiManager.currentGameacquireCoin;
            gameOver = true;
            player.transform.position = player.transform.position;
            player.speed = 0f;
            player.dieEffect.SetActive(true);
            player.tiltSpeed = 0f;
            SaveGameProgress();
        }
    }

    public void SaveGameProgress()
    {
        SaveLoadManager.Data.magnetId = GameData.magnetId;
        SaveLoadManager.Data.protectId = GameData.protectId;
        SaveLoadManager.Data.coinDoubleId = GameData.coinDoubleId;
        SaveLoadManager.Data.invincibilityId = GameData.invincibilityId;
        SaveLoadManager.Data.coin = GameData.coin;
        SaveLoadManager.Data.distanceBestRecord = uiManager.distanceBestRecord;
        SaveLoadManager.Save();
        GameData.GameDataSet();
    }
}
