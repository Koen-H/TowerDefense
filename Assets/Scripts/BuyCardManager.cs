using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BuyCardManager : MonoBehaviour
{
    [SerializeField] TowerSO tower;
    //[SerializeField] GameObject towerPrefab;
    ShopManager shopManager;

    //[SerializeField] MouseManager mouseManager;

    [SerializeField] TextMeshProUGUI towerName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI range;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI AOE;

    [SerializeField] Button buyButton;
    [SerializeField] TextMeshProUGUI buyButtonText;

    public static event System.Action<TowerSO> OnTowerBuy;

    private void Awake()
    {
        if (tower == null)
        {
            Debug.LogError("This card doesn't have a towerPrefab!");
            Destroy(gameObject);
            return;
        }
        GetComponentInChildren<TowerPicture>().SetTowerPicture(tower);
        towerName.text = tower.towerName;
        description.text = tower.description;
        price.text = tower.price.ToString();
        damage.text = tower.projectileDamage.ToString();
        range.text = tower.range.ToString();
        speed.text = tower.shootSpeed.ToString();
        AOE.text = tower.projectileSplashRange.ToString();
        shopManager = GetComponentInParent<ShopManager>();
        ShopManager.OnBalanceChange += CheckIfPurchaseAble;
        ////For now, I suppose
        //Texture2D texture = AssetPreview.GetAssetPreview(towerPrefab);
        //Rect rec = new Rect(0, 0, texture.width, texture.height);
        //towerMesh.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0, 0), 1);
    }
    private void OnDestroy()
    {
        ShopManager.OnBalanceChange -= CheckIfPurchaseAble;
    }

    private void CheckIfPurchaseAble(int currentGold)
    {
        bool purchaseAble = currentGold >= tower.price ? true : false;
        buyButton.enabled = purchaseAble;
        buyButtonText.text = purchaseAble ? "Buy" : "Not enough gold";
    }

    public void Buy()
    {
        shopManager.CloseShop();
        OnTowerBuy?.Invoke(tower);
    }
}
