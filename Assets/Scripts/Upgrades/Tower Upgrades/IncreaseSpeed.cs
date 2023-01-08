using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeed : TowerUpgrade
{
    Tower tower;
    [SerializeField] float[] speedPerLevel;

    public void Start()
    {
        tower = GetComponent<Tower>();
    }

    public override void Upgrade()
    {
        //TODO check for max level
        upgradeLVL++;
        tower.SetSpeed(speedPerLevel[upgradeLVL]);
        Debug.Log("This has been upgraded");
    }
}
