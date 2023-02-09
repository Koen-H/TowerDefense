using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : TowerUpgrade
{

    [SerializeField] float[] speedPerLevel;

    public override void Upgrade()
    {
        if (speedPerLevel.Length <= upgradeLVL + 1) return;
        upgradeLVL++;
        tower.SetSpeed(speedPerLevel[upgradeLVL]);
    }

    public override int GetAmountOfUpgrades()
    {
        return speedPerLevel.Length;
    }
}
