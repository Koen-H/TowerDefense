using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// The tower has shooters, a range and upgrades.
/// The tower is used to communicate with the other components.
/// </summary>
public class Tower : MonoBehaviour
{
    [SerializeField] TowerSO towerData;
    [SerializeField] TowerUpgrade[] upgrades;


    [SerializeField] TargetingType targetingType = TargetingType.First;
    [SerializeField] List<Shooter> shooters;

    SpriteRenderer rangeMesh;

    Range range;

    private void Awake()
    {
        range = GetComponentInChildren<Range>();
        rangeMesh = range.GetComponent<SpriteRenderer>();
        upgrades = GetComponents<TowerUpgrade>();
        LoadTowerData();
    }

    private void LoadTowerData()
    {
        if (towerData == null)
        {
            Debug.LogError("Tower does not have any tower data!");
            return;
        }
        range.SetRange(towerData.range);
        LoadShooterData();
    }
    private void LoadShooterData()
    {
        foreach (Shooter cannon in shooters)
        {
            cannon.SetData(towerData.projectilePrefab, towerData.projectileDamage, towerData.projectilePierces, towerData.projectileSplashRange, towerData.projectileSpeed, towerData.effectDuration, towerData.effectStrength, towerData.projectileRange, towerData.shootSpeed);
        }
    }
    public TowerSO GetTowerData()
    {
        return towerData;
    }

    // Update is called once per frame
    void Update()
    {
        Enemy target = range.GetTarget(targetingType);
        foreach (Shooter cannon in shooters) cannon.SetTarget(target);
    }

    public TowerType GetTowerType()
    {
        return towerData.towerType;
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
    public void SetRange(float newRange)
    {
        range.SetRange(newRange);
        foreach (Shooter cannon in shooters) cannon.CalculateProjectileRange();
    }
    public void SetDamage(float damage)
    {
        foreach (Shooter cannon in shooters) cannon.SetDamage(damage);
    }

    public void SetEffectStrength(float newEffectStrength)
    {
        foreach (Shooter cannon in shooters) cannon.SetEffectStrength(newEffectStrength);
    }

    public void SetEffectDuration(float newDuration)
    {
        foreach (Shooter cannon in shooters) cannon.SetEffectDuration(newDuration);
    }
    public void SetAOE(float newAOE)
    {
        foreach (Shooter cannon in shooters) cannon.SetAOE(newAOE);
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
