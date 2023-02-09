using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectCardManager : MonoBehaviour
{
    Tower inspectingTower;
    [SerializeField] GameObject upgradesListObj;
    [SerializeField] GameObject upgradeCardItemPrefab;
    List<UpgradeCartItem> upgradeCardItems = new List<UpgradeCartItem>();
    public int currentBalance;

    private void Start()
    {
        ShopManager.OnBalanceChange += SetCurrentBalance;
        gameObject.SetActive(false);
    }

    public void OnTowerSelected(Tower selectedTower)
    {
        this.gameObject.SetActive(true);
        if (inspectingTower != null) inspectingTower.ShowRange(false);
        inspectingTower = selectedTower;
        inspectingTower.ShowRange(true);
        GetComponentInChildren<TowerPicture>().SetTowerPicture(inspectingTower.GetTowerData());
        upgradeCardItems.Clear();
        ShowUpgrades();
    }

    private void ShowUpgrades()
    {
        foreach(Transform child in upgradesListObj.transform) { GameObject.Destroy(child.gameObject); }
        if (inspectingTower == null) return;
        TowerUpgrade[] towerUpgrades = inspectingTower.GetUpgrades();
        foreach(TowerUpgrade towerUpgrade in towerUpgrades)
        {

            UpgradeCartItem upgradeCardItem = Instantiate(upgradeCardItemPrefab, upgradesListObj.transform).GetComponent<UpgradeCartItem>();
            upgradeCardItem.SetUpgrade(towerUpgrade);
            upgradeCardItem.UpdateUpgradeButton(currentBalance);
            upgradeCardItems.Add(upgradeCardItem);
        }
    }

    private void SetCurrentBalance(int _currentBalance)
    {
        currentBalance = _currentBalance;
        foreach(UpgradeCartItem upgradeCardItem in upgradeCardItems) upgradeCardItem.UpdateUpgradeButton(currentBalance);
    }

    public void CloseCard()
    {
        inspectingTower.ShowRange(false);
        gameObject.SetActive(false);
    }

}
