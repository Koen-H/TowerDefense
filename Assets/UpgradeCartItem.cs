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
        upgradeButton.onClick.AddListener(() => UpgradeButtonClicked());
        buttonText.text = towerUpgrade.GetCost().ToString();
    }

    void UpgradeButtonClicked()
    {
        //If enough money,
        towerUpgrade.Upgrade();
    }
}
