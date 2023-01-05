using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] TargetingType targetingType = TargetingType.Nearest;
    [SerializeField] List<Shooter> shooters;
    [SerializeField] Range range;

    [SerializeField] SpriteRenderer rangeMesh;





    [Header("Placement Settings")]
    [SerializeField] TowerType towerType = TowerType.Land;

    [Header("Card Settings")]
    [SerializeField] public Sprite stockSprite;
    public string towerName;
    [TextArea]
    public string description;
    public float price;

    private void Awake()
    {
        rangeMesh = range.GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemy target = range.GetTarget(targetingType);
        foreach (Shooter cannon in shooters) cannon.SetTarget(target);
    }

    public TowerType GetTowerType()
    {
        return towerType;
    }


    public void OnMouseDown()
    {
        rangeMesh.enabled = true;
        //Open upgrades
    }
    void OnMouseExit()
    {
        rangeMesh.enabled=false;
    }

    public float GetRange()
    {
        return range.GetRange();
    }
}

public enum TargetingType
{
    First,
    Farthest,
    Nearest,
    Strongest,
}

public enum TowerType
{
    Land,
    Water,
    Shore,
}
