using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// The GameManager is a singleton, it only has one instance and can be reached from anywhere to get a quick reference to the PathGenerator, MouseManager and WaveManager.
/// The GameManager keeps track of the current game condition
/// It keeps track of the amount of health the player has.
/// </summary>
public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public PathGenerator pathGenerator;
    public MouseManager mouseManager;
    public WaveManager waveManager;

    [SerializeField] LevelStatsSO startOfLevelStats;
    int health;
    [SerializeField] TextMeshProUGUI healthDisplay;
    [SerializeField] ConditionManager conditionManager;
    public GameCondition currentGameCondition = GameCondition.Playing;

    /// <summary>
    /// Gets the singleton GameManager
    /// </summary>
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
    }
    private void Start()
    {
        Enemy.OnFinish += OnEnemyFinish;
        WaveManager.OnAllWavesCompleted+=DoGameWin;
        health = startOfLevelStats.starterHealth;
        healthDisplay.text = health.ToString();
    }
    private void OnDestroy()
    {
        Enemy.OnFinish -= OnEnemyFinish;
        WaveManager.OnAllWavesCompleted -= DoGameWin;
    }

    public void Continue()
    {
        health = startOfLevelStats.starterHealth;
        currentGameCondition= GameCondition.Playing;
        waveManager.StartFromWaveOne();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pathGenerator.GeneratePath();
        }
    }

    private void OnEnemyFinish(EnemySO enemy)
    {
        health -= enemy.damageOnGate;
        if (health <= 0) DoGameOver();
        healthDisplay.text = health.ToString();
    }
    private void DoGameOver()
    {
        health = 0;
        currentGameCondition = GameCondition.Lost;
        conditionManager.SetCondition(currentGameCondition);
    }

    public void DoGameWin()
    {
        currentGameCondition = GameCondition.Won;
        conditionManager.SetCondition(currentGameCondition);
    }
}

public enum GameCondition
{
    Playing,
    Won,
    Lost,
    Paused
}
