using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelStats")]
public class LevelStats : ScriptableObject
{
    [SerializeField] int startHealth;
    [SerializeField] int currentHealth;
    [SerializeField] int startGold;
    [SerializeField] int currentGold;
    private void Awake()
    {
        
    }
}
