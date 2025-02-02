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

    private string magnet;
    private string protect;
    private string coinDouble;
    private string invincibility;

    public int magnetId = 1001;
    public int protectId = 2001;
    public int coinDoubleId = 3001;
    public int invincibilityId = 4001;

    private int magnetMaxId = 1005;
    private int protectMaxId = 2005;
    private int coinDoubleMaxId = 3005;
    private int invincibilityMaxId = 4005;

    int coin = 0;

    private void Start()
    {
        coin = uiManager.coin;
        UpdateUpgrade();
    }
    public void OnClickUpgradeMagnet()
    {
        if (magnetId < magnetMaxId)
        {
            ++magnetId;
        }
        UpdateUpgrade();
    }
    public void OnClickUpgradeProtect()
    {
        if (protectId < protectMaxId)
        {
            ++protectId;
        }
        UpdateUpgrade();
    }
    public void OnClickUpgradeCoinDouble()
    {
        if (coinDoubleId < coinDoubleMaxId)
        {
            ++coinDoubleId;
        }
        UpdateUpgrade();
    }
    public void OnClickUpgradeInvincibility()
    {
        if (invincibilityId < invincibilityMaxId)
        {
            ++invincibilityId;
        }
        UpdateUpgrade();
    }
    private void UpdateUpgrade()
    {
        magnet = DataTableManager.UpGradeDataTable.Get(magnetId).Item_Name;
        invincibility = DataTableManager.UpGradeDataTable.Get(invincibilityId).Item_Name;
        coinDouble = DataTableManager.UpGradeDataTable.Get(coinDoubleId).Item_Name;
        protect = DataTableManager.UpGradeDataTable.Get(protectId).Item_Name;

        magnetText.text = magnet;
        invincibilityText.text = invincibility;
        coinDoubleText.text = coinDouble;
        protectText.text = protect;

        magnetCostText.text = DataTableManager.UpGradeDataTable.Get(magnetId).Cost_Value.ToString();
        protectCostText.text = DataTableManager.UpGradeDataTable.Get(protectId).Cost_Value.ToString();
        coinDoubleCostText.text = DataTableManager.UpGradeDataTable.Get(coinDoubleId).Cost_Value.ToString();
        invincibilityCostText.text = DataTableManager.UpGradeDataTable.Get(invincibilityId).Cost_Value.ToString();

        coinText.text = coin.ToString();

    }
    public void OnClickDestory()
    {
        gameObject.SetActive(false);
    }
}
