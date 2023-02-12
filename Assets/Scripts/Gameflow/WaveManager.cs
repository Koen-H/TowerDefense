using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SubWave;

/// <summary>
/// The wavemanager is used to initialize enemies within waves.
/// The wavemanager keeps track of all the enemies that are currently alive
/// </summary>
public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Wave> waves;
    [SerializeField] List<Enemy> enemiesAlive;
    int currentWave;
    Dictionary<int, bool> subwaveSuccesfullySpawned = new ();//int equals subwave spawned, bool equals if succesfulyl spawned.

    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject waveStartButton;
    public static event System.Action<Wave> OnEndOfWave;
    public static event System.Action OnAllWavesCompleted;

    private void Start()
    {
        Enemy.OnEnemyDeath += OnEnemyDeath;

    }
    private void OnDestroy()
    {
        Enemy.OnEnemyDeath-= OnEnemyDeath;
    }

    public void StartWave()
    {
        waveStartButton.SetActive(false);
        PathGenerator.Instance.GeneratePath();//In the future, the path can change mid-game
        waves[currentWave].waveNumber = currentWave +1;
        if(waves[currentWave].waveNumber == waves.Count) waves[currentWave].isLastWave = true;
        InitializeWave(waves[currentWave]);
    }
    public void StartFromWaveOne()
    {
        currentWave= 0;
        StartWave();
    }

    /// <summary>
    /// Starts coroutines for each subwave based on their data.
    /// </summary>
    /// <param name="wave">The wave that will be initialized</param>
    public void InitializeWave(Wave wave)
    {
        subwaveSuccesfullySpawned.Clear();
        int subwaveID = 0;
        foreach (SubWave subwave in wave.subWaves)
        {
            StartCoroutine(SpawnSubWave(subwave, subwaveID));
            subwaveID++;
        }
    }

    /// <summary>
    /// This is a IEnumerator for spawning the subwaves on time.
    /// </summary>
    /// <param name="subWave">The subwave that will spawn</param>
    /// <returns></returns>
    IEnumerator SpawnSubWave(SubWave subWave, int subwaveID)
    {
        subwaveSuccesfullySpawned[subwaveID] = false;
        yield return new WaitForSeconds(subWave.waveStartTime);//We wait before the subwave start
        float timeInbetweenEnemy = subWave.waveDuration / subWave.amount;//Calculate the time between each enemy
        for (int i = 0; i < subWave.amount; i++)
        {
            SpawnEnemy(subWave.enemies);
            if(i != subWave.amount -1) yield return new WaitForSeconds(timeInbetweenEnemy);
        }
        subwaveSuccesfullySpawned[subwaveID] = true;
    }

    /// <summary>
    /// Loops through a dictionary filled with subwave ids and if it's finished spawning the enemies.
    /// </summary>
    /// <returns>Returns true if all the subwaves finished spawning</returns>
    bool AllSubWaveSpawned()
    {
        for(int i = 0; i <= subwaveSuccesfullySpawned.Count - 1; i++)
        {
            if (!subwaveSuccesfullySpawned[i]) return false;
        }
        return true;
    }


    /// <summary>
    /// Checks if all waves are finished spawning when all the enemies are dead.
    /// Will invoke the OnEndOfWave event.
    /// </summary>
    /// <param name="enemy"></param>
    private void OnEnemyDeath(Enemy enemy)
    {
        enemiesAlive.Remove(enemy);
        if(enemiesAlive.Count == 0 && AllSubWaveSpawned())
        {
            OnEndOfWave?.Invoke(waves[currentWave]);
            if (waves[currentWave].isLastWave) OnAllWavesCompleted?.Invoke();
            else currentWave++;
        }
    }

    /// <summary>
    /// Spawns an enemy and sets the data 
    /// </summary>
    /// <param name="enemyTypes"></param>
    void SpawnEnemy(List<EnemySO> enemyTypes)
    {
        Enemy newEnemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
        int random = Random.Range(0,enemyTypes.Count);
        newEnemy.SetEnemyData(enemyTypes[random]);
        enemiesAlive.Add(newEnemy); 
    }
}
