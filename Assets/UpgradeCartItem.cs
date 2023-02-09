using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


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

    void UpdateCard()
    {
        nameText.text = towerUpgrade.GetName();
        descriptionText.text = towerUpgrade.GetDescription();
        lvlText.text = towerUpgrade.GetLVL().ToString();
        

    }

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
        //If enough money,
        towerUpgrade.Upgrade();
        UpdateCard();
        OnTowerUpgrade?.Invoke(towerUpgrade.GetCostOfLVL(towerUpgrade.GetLVL()));

    }
}
