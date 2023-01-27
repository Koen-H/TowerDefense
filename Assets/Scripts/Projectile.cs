using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Tower tower;
    int damage;//How much damage does the projectile deal?
    [SerializeField]float pierces;//How many different enemies can it hit?
    public float speed;//How many different enemies can it hit?
    public float lifeSpanRange;//How far can the projectile reach from the shooter?
    [SerializeField] private StatusEffect statusEffect;//The statusEffect to apply on the enemy.

    [SerializeField] private StatusEffectSO statusEffectSO;


    List<Enemy> enemiesHit = new List<Enemy>();

    private void Awake()
    {
        statusEffect = gameObject.GetComponent<StatusEffect>();//Could become more in future, change to array if it's the case
    }

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
        if (pierces <= 0) return;
        enemiesHit.Add(enemy);
        enemy.Damage(damage);
        if (statusEffect != null) enemy.ApplyStatusEffect(statusEffect);
        //if (statusEffectSO != null) statusEffectSO.ApplyEffect(enemy);
        pierces--;
        if (pierces <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
