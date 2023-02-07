using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public PathGenerator pathGenerator;
    public MouseManager mouseManager;
    public WaveManager waveManager;

    public static GameManager Instance { get {
            if (_instance == null) Debug.LogError("GameManager is null");
            return _instance; 
        } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }


        pathGenerator = this.GetComponent<PathGenerator>();
        mouseManager = this.GetComponent<MouseManager>();
        waveManager = this.GetComponent<WaveManager>();
        Enemy.OnEnemyDeath += Test;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pathGenerator.GeneratePath();
        }
    }

    private void Test (Enemy enemy)
    {
        Debug.Log(enemy.GetEnemyData().goldDrop);
    }
}
