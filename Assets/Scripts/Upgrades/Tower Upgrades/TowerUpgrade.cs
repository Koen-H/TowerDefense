using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerUpgrade : MonoBehaviour
{
    [SerializeField] protected string upgradeName;
    [TextArea]protected string description;
    [SerializeField] protected float baseCost;
    [SerializeField] protected float costMultiplier;
    [SerializeField] protected int upgradeLVL = 0;




    public string GetName() { return upgradeName; }
    public string GetDescription() { return description; }
    public float GetLVL() { return upgradeLVL; }
    public float GetCost() { return baseCost + (costMultiplier * upgradeLVL); }

    public virtual void Upgrade()
    {
        upgradeLVL++;
        Debug.Log("This has been upgraded");
    }
}
