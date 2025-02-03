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

    private string magnetString;
    private string protectString;
    private string coinDoubleString;
    private string invincibilityString;

    public int magnetId = 1001;
    public int protectId = 2001;
    public int coinDoubleId = 3001;
    public int invincibilityId = 4001;

    private int magnetMaxId = 1005;
    private int protectMaxId = 2005;
    private int coinDoubleMaxId = 3005;
    private int invincibilityMaxId = 4005;

    int coin = 0;

    public CoinDouble coinDouble;
    public Invincibility invincibility;
    public Protect protect;
    public Magnet magnet;

    private void Start()
    {
        coin = uiManager.coin;
        UpdateUpgrade();
    }
    public void OnClickUpgradeMagnet()
    {
        var magnetCost = DataTableManager.UpGradeDataTable.Get(magnetId).Cost_Value;
        if (magnetCost < coin)
        {
            if (magnetId < magnetMaxId)
            {
                coin -= magnetCost;
                ++magnetId;
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
        var protectCost = DataTableManager.UpGradeDataTable.Get(protectId).Cost_Value;
        if (protectCost < coin)
        {
            if (protectId < protectMaxId)
            {
                coin -= protectCost;
                ++protectId;
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
        var coinDoubleCost = DataTableManager.UpGradeDataTable.Get(coinDoubleId).Cost_Value;
        if (coinDoubleCost < coin)
        {
            if (coinDoubleId < coinDoubleMaxId)
            {
                coin -= coinDoubleCost;
                ++coinDoubleId;
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
        var invincibilityCost = DataTableManager.UpGradeDataTable.Get(magnetId).Cost_Value;
        if (invincibilityCost < coin)
        {
            if (invincibilityId < invincibilityMaxId)
            {
                coin -= invincibilityCost;
                ++invincibilityId;
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
        magnetString = DataTableManager.UpGradeDataTable.Get(magnetId).Item_Name;
        invincibilityString = DataTableManager.UpGradeDataTable.Get(invincibilityId).Item_Name;
        coinDoubleString = DataTableManager.UpGradeDataTable.Get(coinDoubleId).Item_Name;
        protectString = DataTableManager.UpGradeDataTable.Get(protectId).Item_Name;

        magnetText.text = magnetString;
        invincibilityText.text = invincibilityString;
        coinDoubleText.text = coinDoubleString;
        protectText.text = protectString;

        magnetCostText.text = DataTableManager.UpGradeDataTable.Get(magnetId).Cost_Value.ToString();
        protectCostText.text = DataTableManager.UpGradeDataTable.Get(protectId).Cost_Value.ToString();
        coinDoubleCostText.text = DataTableManager.UpGradeDataTable.Get(coinDoubleId).Cost_Value.ToString();
        invincibilityCostText.text = DataTableManager.UpGradeDataTable.Get(invincibilityId).Cost_Value.ToString();

        coinText.text = coin.ToString();
        coinDouble.time = DataTableManager.UpGradeDataTable.Get(coinDoubleId).Item_Effect;
        protect.time = DataTableManager.UpGradeDataTable.Get(protectId).Item_Effect;
        magnet.time = DataTableManager.UpGradeDataTable.Get(magnetId).Item_Effect;
        invincibility.time = DataTableManager.UpGradeDataTable.Get(invincibilityId).Item_Effect;
    }
    public void OnClickDestory()
    {
        gameObject.SetActive(false);
    }
}
