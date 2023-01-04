using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int health = 1;
    public Walker walker;

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
}
