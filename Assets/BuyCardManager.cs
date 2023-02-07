using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class BuyCardManager : MonoBehaviour
{
    [SerializeField] TowerSO tower;
    //[SerializeField] GameObject towerPrefab;

    [SerializeField] MouseManager mouseManager;

    [SerializeField] TextMeshProUGUI description;

    private void Awake()
    {
        if (tower == null)
        {
            Debug.LogError("This card doesn't have a towerPrefab!");
            Destroy(gameObject);
            return;
        }
        GetComponentInChildren<TowerPicture>().SetTowerPicture(tower);
        description.text = tower.description;
        ////For now, I suppose
        //Texture2D texture = AssetPreview.GetAssetPreview(towerPrefab);
        //Rect rec = new Rect(0, 0, texture.width, texture.height);
        //towerMesh.GetComponent<Image>().sprite = Sprite.Create(texture, rec, new Vector2(0, 0), 1);
    }

    void Start()
    {
        mouseManager = GameManager.Instance.mouseManager;
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Buy()
    {
        //TODO: if currency is not enough, return.
        mouseManager.SetMouseStatus(MouseStatus.Placing);
        mouseManager.placeTowerManager.OnTowerBuy(tower.towerPrefab,tower);
    }
}
