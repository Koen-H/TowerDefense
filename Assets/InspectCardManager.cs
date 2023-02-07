using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectCardManager : MonoBehaviour
{
    Tower inspectingTower;
    [SerializeField] GameObject upgradesListObj;
    [SerializeField] GameObject upgradeCardItemPrefab;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OnTowerSelected(Tower selectedTower)
    {
        this.gameObject.SetActive(true);
        if (inspectingTower != null) inspectingTower.ShowRange(false);
        inspectingTower = selectedTower;
        inspectingTower.ShowRange(true);
        GetComponentInChildren<TowerPicture>().SetTowerPicture(inspectingTower.GetTowerData());
        ShowUpgrades();
    }

    private void ShowUpgrades()
    {
        foreach(Transform child in upgradesListObj.transform) { GameObject.Destroy(child.gameObject); }
        if (inspectingTower == null) return;
        TowerUpgrade[] towerUpgrades = inspectingTower.GetUpgrades();
        foreach(TowerUpgrade towerUpgrade in towerUpgrades)
        {
            Instantiate(upgradeCardItemPrefab, upgradesListObj.transform).GetComponent<UpgradeCartItem>().SetUpgrade(towerUpgrade);
        }
    }

    public void CloseCard()
    {
        inspectingTower.ShowRange(false);
        gameObject.SetActive(false);
    }

}
