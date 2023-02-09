using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseDamage : TowerUpgrade
{
    // Start is called before the first frame update
    [SerializeField] float[] damagePerLevel;

    public override void Upgrade()
    {
        if (damagePerLevel.Length <= upgradeLVL + 1) return;
        upgradeLVL++;
        tower.SetDamage(damagePerLevel[upgradeLVL]);
    }

    public override int GetAmountOfUpgrades()
    {
        return damagePerLevel.Length;
    }
}
