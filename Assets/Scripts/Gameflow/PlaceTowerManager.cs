using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The PlaceTowerManager is used for placing the tower
/// It detects if a tower can be placed
/// </summary>
public class PlaceTowerManager : MonoBehaviour
{
    TowerSO towerToPlace;
    TowerType towerType;
    bool isPlacing = false;
    bool canBePlaced = false;
    GameObject range;
    
    [SerializeField] List<GameObject> colliders = new List<GameObject>();
    SpriteRenderer spriteRenderer;
    public static event System.Action<TowerSO> OnTowerPlace;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        range = transform.Find("Range").gameObject;
        BuyCardManager.OnTowerBuy += OnTowerBuy;
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        BuyCardManager.OnTowerBuy -= OnTowerBuy;

    }

    // Update is called once per frame
    void Update()
    {
        if (isPlacing)
        {
            CheckPlacement();
            if (Input.GetMouseButtonDown(0) && canBePlaced) PlaceTower();
            if (Input.GetMouseButtonDown(1)) CancelTower();

        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        colliders.Add(other.gameObject);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        colliders.Remove(other.gameObject);
    }

    /// <summary>
    /// Checks the placement by comparing the amount of colliders, then checks if the collider tag equals the type of the tower.
    /// </summary>
    void CheckPlacement()
    {
        if (colliders.Count != 1 || colliders[0].tag == "Tower" || colliders[0].tag == "Path") {//Can't be placed
            spriteRenderer.color = Color.red;
            canBePlaced = false;
            return; 
        }
        if ((colliders[0].tag == "Land" && TowerType.Land == towerType) || (colliders[0].tag == "Water" && TowerType.Water == towerType))//There's space and it matches the specific tower
        {
            spriteRenderer.color = Color.green;
            canBePlaced = true;
        }
    }
    /// <summary>
    /// The tower is selected to be bought, the gameobjects becomes active and has the correct data to help visualize where the player places the tower.
    /// 
    /// </summary>
    /// <param name="towerBought"></param>
    public void OnTowerBuy(TowerSO towerBought)
    {
        gameObject.SetActive(true);
        //towerToPlacePrefab = towerBoughtPrefab;
        towerToPlace = towerBought;
        towerType = towerBought.GetTowerType();
        isPlacing = true;
        spriteRenderer.sprite = towerBought.stockSprite;
        float towerRange = towerBought.GetRange();
        range.transform.localScale = new Vector3(towerRange, towerRange, towerRange);
    }


    void PlaceTower()
    {
        OnTowerPlace.Invoke(towerToPlace);
        Instantiate(towerToPlace.towerPrefab,transform.position,Quaternion.identity);
        isPlacing = false;
        canBePlaced = false;
        //gameManager.mouseManager.SetMouseStatus(MouseStatus.Idle);
        gameObject.SetActive(false);
    }

    void CancelTower()
    {
        isPlacing = false;
        canBePlaced = false;
        //gameManager.mouseManager.SetMouseStatus(MouseStatus.Idle);
        gameObject.SetActive(false);
    }
}
