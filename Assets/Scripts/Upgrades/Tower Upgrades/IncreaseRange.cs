using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseRange : TowerUpgrade
{
    [SerializeField] float[] rangePerLevel;

    public override void Upgrade()
    {
        if (rangePerLevel.Length <= upgradeLVL + 1) return;
        upgradeLVL++;
        tower.SetRange(rangePerLevel[upgradeLVL]);
        Debug.Log("This has been upgraded");
    }
    public override int GetAmountOfUpgrades()
    {
        return rangePerLevel.Length;
    }
}
