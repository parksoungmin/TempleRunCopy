using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUi : MonoBehaviour
{
    public UiManager uiManager;

    public TextMeshProUGUI coinText;

    public TextMeshProUGUI magnetCostText;
    public TextMeshProUGUI protectCostText;
    public TextMeshProUGUI coinDoubleCostText;
    public TextMeshProUGUI invincibilityCostText;

    public TextMeshProUGUI magnetExplanationText;
    public TextMeshProUGUI protectExplanationText;
    public TextMeshProUGUI coinDoubleExplanationText;
    public TextMeshProUGUI invincibilityExpanationText;

    public GameManager gameManager;

    private readonly int magnetMaxId = 1005;
    private readonly int protectMaxId = 2005;
    private readonly int coinDoubleMaxId = 3005;
    private readonly int invincibilityMaxId = 4005;

    public CoinDouble coinDouble;
    public Invincibility invincibility;
    public Protect protect;
    public Magnet magnet;

    public Toggle[] magnetUpgradPoint;
    public Toggle[] protectUpgradPoint;
    public Toggle[] coinDoubleUpgradPoint;
    public Toggle[] invincibilityUpgradPoint;

    private int magnetCurrentToggleIndex;
    private int protectCurrentToggleIndex;
    private int coinDoubleCurrentToggleIndex;
    private int invincibilityCurrentToggleIndex;

    private void OnEnable()
    {
        UpdateUpgrade();
    }
    private void Start()
    {
        for (int i = 0; i<magnetCurrentToggleIndex; i++)
        {
            magnetUpgradPoint[i].isOn = true;
        }
        for (int i = 0; i<protectCurrentToggleIndex; i++)
        {
            protectUpgradPoint[i].isOn = true;
        }
        for (int i = 0; i<coinDoubleCurrentToggleIndex; i++)
        {
            coinDoubleUpgradPoint[i].isOn = true;
        }
        for (int i = 0; i<invincibilityCurrentToggleIndex; i++)
        {
            invincibilityUpgradPoint[i].isOn = true;
        }
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
                for (int i = 0; i<magnetCurrentToggleIndex; i++)
                {
                    magnetUpgradPoint[i].isOn = true;
                }
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
                for (int i = 0; i<protectCurrentToggleIndex; i++)
                {
                    protectUpgradPoint[i].isOn = true;
                }
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
                for (int i = 0; i<coinDoubleCurrentToggleIndex; i++)
                {
                    coinDoubleUpgradPoint[i].isOn = true;
                }
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
                for (int i = 0; i<invincibilityCurrentToggleIndex; i++)
                {
                    invincibilityUpgradPoint[i].isOn = true;
                }
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
        magnetCostText.text = DataTableManager.UpGradeDataTable.Get(GameData.magnetId).Cost_Value.ToString();
        protectCostText.text = DataTableManager.UpGradeDataTable.Get(GameData.protectId).Cost_Value.ToString();
        coinDoubleCostText.text = DataTableManager.UpGradeDataTable.Get(GameData.coinDoubleId).Cost_Value.ToString();
        invincibilityCostText.text = DataTableManager.UpGradeDataTable.Get(GameData.invincibilityId).Cost_Value.ToString();

        coinText.text = GameData.coin.ToString();
        coinDouble.time = DataTableManager.UpGradeDataTable.Get(GameData.coinDoubleId).Item_Effect;
        protect.time = DataTableManager.UpGradeDataTable.Get(GameData.protectId).Item_Effect;
        magnet.time = DataTableManager.UpGradeDataTable.Get(GameData.magnetId).Item_Effect;
        invincibility.time = DataTableManager.UpGradeDataTable.Get(GameData.invincibilityId).Item_Effect;

        magnetCurrentToggleIndex = GameData.magnetId % 1000;
        protectCurrentToggleIndex = GameData.protectId % 2000;
        coinDoubleCurrentToggleIndex = GameData.coinDoubleId % 3000;
        invincibilityCurrentToggleIndex = GameData.invincibilityId % 4000;

        magnetExplanationText.text = $"The player gains magnetic powers. \nLasts for {magnet.time} seconds";
        protectExplanationText.text = $"Prevents player death once and is \nactive for {protect.time} seconds";
        coinDoubleExplanationText.text = $"Coins earned by the player are doubled and \nlast for {coinDouble.time} seconds.";
        invincibilityExpanationText.text = $"The player becomes invincible. \nLasts for {invincibility.time} seconds.";

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
