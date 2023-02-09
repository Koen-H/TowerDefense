using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseEffectStrength : TowerUpgrade
{

    [SerializeField] float[] effectStrengthPerLevel;

    public override void Upgrade()
    {
        if (effectStrengthPerLevel.Length <= upgradeLVL + 1) return;
        upgradeLVL++;
        tower.SetEffectStrength(effectStrengthPerLevel[upgradeLVL]);
        Debug.Log("This has been upgraded");
    }

    public override int GetAmountOfUpgrades()
    {
        return effectStrengthPerLevel.Length;
    }
}
