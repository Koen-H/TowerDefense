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
    int health = 1;
    public Walker walker;
    public Dictionary<Type, StatusEffect> statusEffects= new ();
    private SpriteRenderer spriteRenderer;
    public bool ignoredByCannons;

    public static event System.Action<Enemy> OnEnemyDeath;

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

    public int GetHealth()
    {
        return health;
    }

    public void OnReachedFinish()
    {
        ignoredByCannons = true;
        //TODO: Decrease health.
    }
    public void OnReachedEnd()
    {
        Destroy(gameObject);
    }

    public void Damage(int damage)
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
        //TODO: Add gold
        OnEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }

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

    internal void StartCoroutine(object innerEffect)
    {
        throw new NotImplementedException();
    }
}
