using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseEffectDuration : TowerUpgrade
{

    [SerializeField] float[] effectDurationPerLevel;

    public override void Upgrade()
    {
        if (effectDurationPerLevel.Length <= upgradeLVL + 1) return;
        upgradeLVL++;
        tower.SetEffectDuration(effectDurationPerLevel[upgradeLVL]);
    }

    public override int GetAmountOfUpgrades()
    {
        return effectDurationPerLevel.Length;
    }
}
