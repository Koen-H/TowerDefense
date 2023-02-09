using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

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
    private void LoadEnemyData()
    {
        if (enemyData == null)
        {
            Debug.LogError("Tower does not have any tower data!");
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

    public void OnReachedFinish()//the enemy officially made it and the health drops, but stays alive for the visual aspect
    {
        ignoredByCannons = true;
        OnFinish.Invoke(enemyData);
        
    }
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

    public void ApplyStatusEffect(StatusEffect newEffect)
    {
        Debug.Log($"newEffect has a strength of { newEffect.GetStrength()}");
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
