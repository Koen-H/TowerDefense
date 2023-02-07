using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TowerSO")]
public class TowerSO : ScriptableObject
{
    public GameObject towerPrefab;

    [Header("Placement Settings")]
    [SerializeField] public TowerType towerType = TowerType.Land;

    [Header("Range Settings")]
    public float range;

    [Header("Shooter Settings")]
    public GameObject projectilePrefab;//The projectile that will be shot?
    public int projectileDamage;//How much damage does the projectile do?
    public int projectilePierces;//How many different enemies can it hit?
    public float projectileSplashRange;//Does it have a AEO? And how big ?
    public float projectileSpeed;//How fast does the projectile fly?
    public float effectDuration;//How long does the effect last?
    public float effectStrength;//How strong is the effect?
    public float projectileRange;//How far do the projectiles go beyond the range of the tower?
    public float shootSpeed;//How fast can it shoot?
    [Space(5)]
    [Header("--==Projectile StatusEffects are located under the prefab==--")]
    [Space(5)]



    [Space(5)]
    [Header("--==Upgrades are located under the prefab==--")]
    [Space (5)]

    [Header("Card Settings")]
    [SerializeField] public Sprite stockSprite;
    public string towerName;
    [TextArea]
    public string description;
    public float price;


    public float GetRange()
    {
        return range;
    }

    public TowerType GetTowerType()
    {
        return towerType;
    }
}
