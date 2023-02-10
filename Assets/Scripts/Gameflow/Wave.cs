using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]


public class Wave //Future improvement could be a scriptable object!
{
    [HideInInspector] public int waveNumber;
    [HideInInspector] public bool isLastWave;
    [SerializeField] public List<SubWave> subWaves;
    [SerializeField] public int waveCompletionGoldBonus;
    [SerializeField] public int breakTimeAfterCompletion;
}
