using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// The enemy class keeps track of everything related to the enemy.
/// The enemy class keeps track of health.
/// The enemy has a walker, which is used to walk along the path.
/// </summary>
public class Enemy : MonoBehaviour
{
    [SerializeField] EnemySO enemyData;
    [SerializeField]
    float health = 1;
    public Walker walker;
    public Dictionary<Type, StatusEffect> statusEffects= new ();
    private SpriteRenderer spriteRenderer;
    public bool ignoredByCannons;

    public static event System.Action<Enemy> OnEnemyDeath;
    public static event System.Action<EnemySO> OnFinish;

    private void Start()
    {
        walker = GetComponent<Walker>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        LoadEnemyData();
    }
    /// <summary>
    /// Loads the enemy data
    /// </summary>
    private void LoadEnemyData()
    {
        if (enemyData == null)
        {
            Debug.LogError("Enemy does not have any Enemy data!");
            return;
        }
        health = enemyData.defaultHealth;
        walker.SetSpeed(enemyData.speed);
        LoadCorrectSprite();
    }
    public EnemySO GetEnemyData()
    {
        return enemyData;
    }

    public void SetEnemyData(EnemySO _enemyData)
    {
        enemyData = _enemyData;
    }

    public float GetHealth()
    {
        return health;
    }

    /// <summary>
    /// The enemy reached the finish, which is the gate. It stays alive for visual aspect but gets ignored by cannons
    /// </summary>
    public void OnReachedFinish()
    {
        ignoredByCannons = true;
        OnFinish.Invoke(enemyData);
        
    }
    /// <summary>
    /// When it's off screen it reaches the end of the path and it will die.
    /// </summary>
    public void OnReachedEnd()
    {
        Die();//Also giving the player gold because I'm nice and they most likely need it if the enemy reached this point because they suck at the game.
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0) Die();
        LoadCorrectSprite();
    }

    /// <summary>
    /// Loads the correct sprite based on their current health.
    /// </summary>
    private void LoadCorrectSprite()
    {
        spriteRenderer.sprite = enemyData.GetCorrectSprite(health);
    }

    protected void Die()
    {
        OnEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }

    /// <summary>
    /// If the effect is already on the enemy it will try to reset it if possible.
    /// 
    /// </summary>
    /// <param name="newEffect"></param>
    public void ApplyStatusEffect(StatusEffect newEffect)
    {
        if (statusEffects.TryGetValue(newEffect.GetType(), out StatusEffect oldEffect))
        {
            oldEffect.ResetEffect(newEffect);
        }
        else
        {
            statusEffects[newEffect.GetType()] = gameObject.AddComponent(newEffect.GetType()) as StatusEffect;
            statusEffects[newEffect.GetType()].CopyFrom(newEffect);
        }
    }
}
