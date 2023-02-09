using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerUpgrade : MonoBehaviour
{
    protected Tower tower;
    [SerializeField] protected string upgradeName;
    [TextArea]protected string description;
    [SerializeField] protected int baseCost;
    [SerializeField] protected float costMultiplier;
    [SerializeField] protected int upgradeLVL = 0;

    //FUTURE IMPROVEMENT, make a new class that inherits from this class which is a statistical upgrade.(as all my current one are just changing numbers)

    public void Start() { tower = GetComponent<Tower>(); }
    public string GetName() { return upgradeName; }
    public string GetDescription() { return description; }
    public int GetLVL() { return upgradeLVL; }
    public int GetCost() {return (int)(baseCost * (costMultiplier * (upgradeLVL + 1))); }//plus one as we are calculating the cost of the NEXT level
    /// <summary>
    /// Returns the cost of the requested lvl
    /// </summary>
    /// <returns></returns>
    public int GetCostOfLVL(int level) { return (int)(baseCost * (costMultiplier * (level))); }

    public virtual void Upgrade()
    {
        upgradeLVL++;
        Debug.Log("This has been upgraded, but no upgrade is implemented");
    }

    public virtual int GetAmountOfUpgrades()
    {
        Debug.LogError("There is no max amount of upgrades set for this upgrade!");
        return 0;
    }
}
