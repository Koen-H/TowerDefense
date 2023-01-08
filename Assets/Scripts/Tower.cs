using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;

public class Tower : MonoBehaviour
{
    [SerializeField] TargetingType targetingType = TargetingType.Nearest;
    [SerializeField] List<Shooter> shooters;
    [SerializeField] Range range;

    [SerializeField] SpriteRenderer rangeMesh;

    [SerializeField] TowerUpgrade[] upgrades;



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
        upgrades = GetComponents<TowerUpgrade>();
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


    public float GetRange()
    {
        return range.GetRange();
    }
    public void ShowRange(bool show)
    {
        range.ShowRange(show);
    }

    public void SetSpeed(float speed)
    {
        foreach (Shooter cannon in shooters) cannon.SetSpeed(speed);
    }

   public TowerUpgrade[] GetUpgrades() { return upgrades; }
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
