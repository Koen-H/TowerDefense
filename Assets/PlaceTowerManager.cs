using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowerManager : MonoBehaviour
{
    Tower towerToPlace;
    GameObject towerToPlacePrefab;
    TowerType towerType;
    GameManager gameManager;
    bool isPlacing = false;
    bool canBePlaced = false;
    GameObject range;
    
    [SerializeField] List<GameObject> colliders = new List<GameObject>();
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        range = transform.Find("Range").gameObject;
    }
    private void Start()
    {
        gameManager = GameManager.Instance;
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

    public void OnTowerBuy(GameObject towerBoughtPrefab, Tower towerBought)
    {
        gameObject.SetActive(true);
        towerToPlacePrefab = towerBoughtPrefab;
        towerToPlace = towerBought;
        towerType = towerBought.GetTowerType();
        isPlacing = true;
        spriteRenderer.sprite = towerBought.stockSprite;
        float towerRange = towerBought.GetRange();
        range.transform.localScale = new Vector3(towerRange, towerRange, towerRange);
    }

    void PlaceTower()
    {
        //TODO: Remove currency from balance
        Instantiate(towerToPlace,transform.position,Quaternion.identity);
        isPlacing = false;
        canBePlaced = false;
        gameManager.mouseManager.SetMouseStatus(MouseStatus.Idle);
        gameObject.SetActive(false);
    }

    void CancelTower()
    {
        
        isPlacing = false;
        canBePlaced = false;
        gameManager.mouseManager.SetMouseStatus(MouseStatus.Idle);
        gameObject.SetActive(false);
    }
}
