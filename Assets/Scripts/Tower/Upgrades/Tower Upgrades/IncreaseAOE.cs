using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAOE: TowerUpgrade
{

    [SerializeField] float[] AOEPerLevel;

    public override void Upgrade()
    {
        if (AOEPerLevel.Length <= upgradeLVL + 1) return;
        upgradeLVL++;
        tower.SetAOE(AOEPerLevel[upgradeLVL]);
    }

    public override int GetAmountOfUpgrades()
    {
        return AOEPerLevel.Length;
    }
}
