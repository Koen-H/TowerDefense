using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] public Tower tower;
    [SerializeField] int damage;//How much damage does the projectile deal?
    [SerializeField] float pierces;//How many different enemies can it hit?
    [SerializeField] public float speed;//How many different enemies can it hit?
    [SerializeField] public float lifeSpanRange;//How far can the projectile reach from the shooter?

    List<Enemy> enemiesHit = new List<Enemy>();

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy)) Hit(enemy);
    }

    void Update()
    {
        if ((tower.transform.position - transform.position).magnitude > lifeSpanRange) Die();
    }

    private void Hit(Enemy enemy)
    {
        enemiesHit.Add(enemy);
        enemy.Damage(damage);
        pierces--;
        if (pierces <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
