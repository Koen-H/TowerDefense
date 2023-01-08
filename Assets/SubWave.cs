using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SubWave
{

    public EnemyType enemies;//The type of enemies that can spawn in the wave
    public int amount;//How many enemies will be in this wave
    public float waveDuration;//Total time the wave can take
    public float waveStartTime;
    [System.Flags]
    public enum EnemyType
    {
        WeakRowboat = 1,
        DamagedRowboat = 2,
        Rowboat = 4,
    }
}
