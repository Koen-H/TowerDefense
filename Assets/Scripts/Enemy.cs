using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 1;
    public Walker walker;
    List<StatusEffect> statusEffects= new List<StatusEffect>();

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

    public void ApplyStatusEffect(StatusEffect effect)
    {
        if (TryGetComponent<StatusEffect>(out effect) )
        {
            effect.ResetDuration();
        }
        else statusEffects.Add(this.AddComponent<StatusEffect>());
    }
}
