using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A UpgradeCartItem is used to display and upgrade tower upgrades within the InspectCard, it also manages the functionality of the button
/// </summary>
public class UpgradeCartItem : MonoBehaviour
{
    TowerUpgrade towerUpgrade;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] TextMeshProUGUI lvlText;

    [SerializeField] Button upgradeButton;
    [SerializeField] TextMeshProUGUI buttonText;

    public static event System.Action<int> OnTowerUpgrade;
    

    public void SetUpgrade(TowerUpgrade _towerUpgrade)
    {
        towerUpgrade = _towerUpgrade;
        UpdateCard();
    }

    /// <summary>
    /// Updates the UI with the correct information.
    /// </summary>
    void UpdateCard()
    {
        nameText.text = towerUpgrade.GetName();
        descriptionText.text = towerUpgrade.GetDescription();
        lvlText.text = towerUpgrade.GetLVL().ToString();
    }

    /// <summary>
    /// Updates the status of the button, such as changing the color or disabling the ability to upgrade based on the current balance.
    /// </summary>
    /// <param name="currentBalance">The current balance the player has.</param>
    public void UpdateUpgradeButton(int currentBalance)
    {
        upgradeButton.onClick.RemoveAllListeners();
        if (towerUpgrade.GetLVL() >= towerUpgrade.GetAmountOfUpgrades() - 1)
        {
            buttonText.text = "MAX LVL";
        }
        else
        {
            buttonText.text = towerUpgrade.GetCost().ToString();
            if (currentBalance >= towerUpgrade.GetCost())
            {
                buttonText.color = Color.black;
                upgradeButton.onClick.AddListener(() => UpgradeButtonClicked());
            }
            else
            {
                buttonText.color = Color.red;
            }
        }
    }

    void UpgradeButtonClicked()
    {
        towerUpgrade.Upgrade();
        UpdateCard();
        OnTowerUpgrade?.Invoke(towerUpgrade.GetCostOfLVL(towerUpgrade.GetLVL()));

    }
}
