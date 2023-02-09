using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SubWave;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Wave> waves;
    [SerializeField] List<Enemy> enemiesAlive;
    int currentWave;
    Dictionary<int, bool> subwaveSuccesfullySpawned = new ();//int equals subwave spawned, bool equals if succesfulyl spawned.

    [SerializeField] GameObject enemyPrefab;//For now just one
    [SerializeField] GameObject waveStartButton;
    public static event System.Action<Wave> OnEndOfWave;
    public static event System.Action OnAllWavesCompleted;

    private void Start()
    {
        Enemy.OnEnemyDeath += OnEnemyDeath;

    }

    public void StartWave()
    {
        waveStartButton.SetActive(false);
        GameManager.Instance.pathGenerator.GeneratePath();//In the future, the path can change mid-game
        waves[currentWave].waveNumber = currentWave +1;
        if(waves[currentWave].waveNumber == waves.Count) waves[currentWave].isLastWave = true;
        InitializeWave(waves[currentWave]);
    }

    public void InitializeWave(Wave wave)
    {
        subwaveSuccesfullySpawned.Clear();
        int subwaveID = 0;
        foreach (SubWave subwave in wave.subWaves)
        {
            StartCoroutine(SpawnWave(subwave, subwaveID));
            subwaveID++;
        }
    }

    /// <summary>
    /// This is a IEnumerator for spawning the subwaves on time.
    /// </summary>
    /// <param name="wave"></param>
    /// <returns></returns>
    IEnumerator SpawnWave(SubWave wave, int subwaveID)
    {
        subwaveSuccesfullySpawned[subwaveID] = false;
        yield return new WaitForSeconds(wave.waveStartTime);//We wait before the subwave start
        float timeInbetweenEnemy = wave.waveDuration / wave.amount;//Calculate the time between each enemy
        for (int i = 0; i < wave.amount; i++)
        {
            SpawnEnemy(wave.enemies);
            if(i != wave.amount -1) yield return new WaitForSeconds(timeInbetweenEnemy);
        }
        subwaveSuccesfullySpawned[subwaveID] = true;
    }

    bool AllSubWaveSpawned()
    {
        for(int i = 0; i <= subwaveSuccesfullySpawned.Count - 1; i++)
        {
            Debug.Log($"{subwaveSuccesfullySpawned[i]} {i}");
            if (!subwaveSuccesfullySpawned[i]) return false;
        }
        return true;
    }


    private void OnEnemyDeath(Enemy enemy)
    {
        enemiesAlive.Remove(enemy);
        if(enemiesAlive.Count == 0 && AllSubWaveSpawned())
        {
            OnEndOfWave?.Invoke(waves[currentWave]);
            if (waves[currentWave].isLastWave) { OnAllWavesCompleted?.Invoke(); }
            currentWave++;
            Debug.Log("This is the end of the wave!");
        }
    }


    void SpawnEnemy(List<EnemySO> enemyTypes)
    {
        Enemy newEnemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
        int random = Random.Range(0,enemyTypes.Count);
        newEnemy.SetEnemyData(enemyTypes[random]);
        enemiesAlive.Add(newEnemy); 
    }
}
