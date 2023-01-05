using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Wave
{
    public EnemyType enemies;//The type of enemies that can spawn in the wave
    public float waveDuration;//Total time the wave can take
    public float enemySpawnInterval;//Minimum time between spawning

}
[System.Flags]
public enum EnemyType
{
    WeakRowboat = 1,
    DamagedRowboat = 2,
    Rowboat = 4,
}