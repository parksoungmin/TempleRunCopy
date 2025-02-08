using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeUi : MonoBehaviour
{
    public UiManager uiManager;

    public TextMeshProUGUI coinText;

    public TextMeshProUGUI magnetText;
    public TextMeshProUGUI protectText;
    public TextMeshProUGUI coinDoubleText;
    public TextMeshProUGUI invincibilityText;

    public TextMeshProUGUI magnetCostText;
    public TextMeshProUGUI protectCostText;
    public TextMeshProUGUI coinDoubleCostText;
    public TextMeshProUGUI invincibilityCostText;

    public GameManager gameManager;

    private string magnetString;
    private string protectString;
    private string coinDoubleString;
    private string invincibilityString;


    private int magnetMaxId = 1005;
    private int protectMaxId = 2005;
    private int coinDoubleMaxId = 3005;
    private int invincibilityMaxId = 4005;

    public CoinDouble coinDouble;
    public Invincibility invincibility;
    public Protect protect;
    public Magnet magnet;

    private void OnEnable()
    {
        UpdateUpgrade();
    }

    public void OnClickUpgradeMagnet()
    {
        var magnetCost = DataTableManager.UpGradeDataTable.Get(GameData.magnetId).Cost_Value;
        if (magnetCost < GameData.coin)
        {
            if (GameData.magnetId < magnetMaxId)
            {
                GameData.coin -= magnetCost;
                ++GameData.magnetId;
                UpdateUpgrade();
            }
            else
            {
                Debug.Log("강화가 최대치입니다.");
            }
        }
        else
        {
            Debug.Log("coin이 모자랍니다.");
        }
    }
    public void OnClickUpgradeProtect()
    {
        var protectCost = DataTableManager.UpGradeDataTable.Get(GameData.protectId).Cost_Value;
        if (protectCost < GameData.coin)
        {
            if (GameData.protectId < protectMaxId)
            {
                GameData.coin -= protectCost;
                ++GameData.protectId;
                UpdateUpgrade();
            }
            else
            {
                Debug.Log("강화가 최대치입니다.");
            }
        }
        else
        {
            Debug.Log("coin이 모자랍니다.");
        }
    }
    public void OnClickUpgradeCoinDouble()
    {
        var coinDoubleCost = DataTableManager.UpGradeDataTable.Get(GameData.coinDoubleId).Cost_Value;
        if (coinDoubleCost < GameData.coin)
        {
            if (GameData.coinDoubleId < coinDoubleMaxId)
            {
                GameData.coin -= coinDoubleCost;
                ++GameData.coinDoubleId;
                UpdateUpgrade();
            }
            else
            {
                Debug.Log("강화가 최대치입니다.");
            }
        }
        else
        {
            Debug.Log("coin이 모자랍니다.");
        }
    }
    public void OnClickUpgradeInvincibility()
    {
        var invincibilityCost = DataTableManager.UpGradeDataTable.Get(GameData.invincibilityId).Cost_Value;
        if (invincibilityCost < GameData.coin)
        {
            if (GameData.invincibilityId < invincibilityMaxId)
            {
                GameData.coin -= invincibilityCost;
                ++GameData.invincibilityId;
                UpdateUpgrade();
            }
            else
            {
                Debug.Log("강화가 최대치입니다.");
            }
        }
        else
        {
            Debug.Log("coin이 모자랍니다.");
        }
    }
    private void UpdateUpgrade()
    {
        gameManager.SaveGameProgress();

        magnetString = DataTableManager.UpGradeDataTable.Get(GameData.magnetId).Item_Name;
        invincibilityString = DataTableManager.UpGradeDataTable.Get(GameData.invincibilityId).Item_Name;
        coinDoubleString = DataTableManager.UpGradeDataTable.Get(GameData.coinDoubleId).Item_Name;
        protectString = DataTableManager.UpGradeDataTable.Get(GameData.protectId).Item_Name;

        magnetText.text = magnetString;
        invincibilityText.text = invincibilityString;
        coinDoubleText.text = coinDoubleString;
        protectText.text = protectString;

        magnetCostText.text = DataTableManager.UpGradeDataTable.Get(GameData.magnetId).Cost_Value.ToString();
        protectCostText.text = DataTableManager.UpGradeDataTable.Get(GameData.protectId).Cost_Value.ToString();
        coinDoubleCostText.text = DataTableManager.UpGradeDataTable.Get(GameData.coinDoubleId).Cost_Value.ToString();
        invincibilityCostText.text = DataTableManager.UpGradeDataTable.Get(GameData.invincibilityId).Cost_Value.ToString();

        coinText.text = GameData.coin.ToString();
        coinDouble.time = DataTableManager.UpGradeDataTable.Get(GameData.coinDoubleId).Item_Effect;
        protect.time = DataTableManager.UpGradeDataTable.Get(GameData.protectId).Item_Effect;
        magnet.time = DataTableManager.UpGradeDataTable.Get(GameData.magnetId).Item_Effect;
        invincibility.time = DataTableManager.UpGradeDataTable.Get(GameData.invincibilityId).Item_Effect;

    }
    public void OnClickDestory()
    {
        gameObject.SetActive(false);
    }
    public void OnClickOpen()
    {
        gameObject.SetActive(true);
    }
}
