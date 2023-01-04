using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceTowerManager : MonoBehaviour
{
    [SerializeField] bool isLandTower;
    [SerializeField] bool isWaterTower;
    
    [SerializeField] List<GameObject> colliders = new List<GameObject>();
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;

        CheckPlacement();
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
        if(colliders.Count != 1 || colliders[0].tag == "Tower") {//Can't be placed
            spriteRenderer.color = Color.red;
            return; 
        }
        if ((colliders[0].tag == "Land" && isLandTower) || (colliders[0].tag == "Water" && isWaterTower))//There's space and it matches the specific tower
        {
            spriteRenderer.color = Color.blue;
        }
    }
}
