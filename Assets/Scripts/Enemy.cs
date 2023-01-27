using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 1;
    public Walker walker;
    Dictionary<Type, StatusEffect> statusEffects= new ();

    private void Awake()
    {
        walker = GetComponent<Walker>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int GetHealth()
    {
        return health;
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    protected void Die()
    {
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
        }



    }

    internal void StartCoroutine(object innerEffect)
    {
        throw new NotImplementedException();
    }
}
