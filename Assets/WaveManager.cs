using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SubWave;

public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Wave> waves;
    [SerializeField] List<Enemy> ememiesAlive;
    int currentWave;

    [SerializeField] GameObject enemyPrefab;//For now just one

    private void Start()
    {
        Enemy.OnEnemyDeath += OnEnemyDeath;
    }

    private void Update()
    {
        Debug.Log(ememiesAlive.Count);
    }
    public void StartWave()
    {
        GameManager.Instance.pathGenerator.GeneratePath();//In the future, the path can change mid-game
        InitializeWave(waves[currentWave]);
        
    }

    public void InitializeWave(Wave wave)
    {
        foreach (SubWave subwave in wave.subWaves) StartCoroutine(SpawnWave(subwave));
    }

    IEnumerator SpawnWave(SubWave wave)
    {
        yield return new WaitForSeconds(wave.waveStartTime);//We wait before the wave start
        float timeInbetweenEnemy = wave.waveDuration / wave.amount;//Calculate the time between each enemy
        for (int i = 0; i < wave.amount; i++)
        {
            SpawnEnemy(wave.enemies);
            yield return new WaitForSeconds(timeInbetweenEnemy);
        }
    }
    private void OnEnemyDeath(Enemy enemy)
    {
       ememiesAlive.Remove(enemy);

    }


    void SpawnEnemy(List<EnemySO> enemyTypes)
    {
        Enemy newEnemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
        int random = Random.Range(0,enemyTypes.Count);
        newEnemy.SetEnemyData(enemyTypes[random]);
        ememiesAlive.Add(newEnemy); 
    }
}
