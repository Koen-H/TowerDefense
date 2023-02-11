using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The complete shop, containing the current balance and all the methods to change it.
/// Runs completely on events
/// </summary>
public class ShopManager : MonoBehaviour
{
    [SerializeField] LevelStatsSO levelStats;
    int currentGold = 0;

    public static event System.Action<int> OnBalanceChange;
    public static event System.Action<int> OnBalanceIncrease;
    private void Awake()
    {
        Enemy.OnEnemyDeath += OnEnemyDeath;
        PlaceTowerManager.OnTowerPlace += OnTowerPlace;
        UpgradeCartItem.OnTowerUpgrade += OnTowerUpgrade;
        WaveManager.OnEndOfWave += OnEndOfWave;
    }

    private void OnDestroy()
    {
        Enemy.OnEnemyDeath -= OnEnemyDeath;
        PlaceTowerManager.OnTowerPlace -= OnTowerPlace;
        UpgradeCartItem.OnTowerUpgrade -= OnTowerUpgrade;
        WaveManager.OnEndOfWave -= OnEndOfWave;
    }

    private void Start()
    {
        SetBalance(levelStats.starterGold);
        this.gameObject.SetActive(false);
    }

    public void AddBalance(int addAmount)
    {
        SetBalance(currentGold + addAmount);
        OnBalanceIncrease?.Invoke(addAmount);
    }

    public void RemoveBalance(int removeAmount)
    {
        SetBalance(currentGold - removeAmount);
    }

    private void SetBalance(int newBalance)
    {
        currentGold = newBalance;
        OnBalanceChange?.Invoke(currentGold);
    }

    public int GetBalance()
    {
        return currentGold;
    }

    void OnTowerPlace(TowerSO tower)
    {
        RemoveBalance(tower.price);
    }

    void OnTowerUpgrade(int cost)
    {
        RemoveBalance(cost);
    }

    void OnEnemyDeath(Enemy enemy)
    {
        AddBalance(enemy.GetEnemyData().goldDrop);
    }
    void OnEndOfWave(Wave wave)
    {
        AddBalance(wave.waveCompletionGoldBonus);
    }
}
