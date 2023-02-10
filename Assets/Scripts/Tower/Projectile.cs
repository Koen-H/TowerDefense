using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The projectile has the movement script and applies damage on collision.
/// It has all the values a projectile needs.
/// </summary>
public class Projectile : MonoBehaviour
{
    public Tower tower;
    private float damage;//How much damage does the projectile deal?
    [SerializeField] int pierces;//How many different enemies can it hit?
    private float splashRange = 0;//If the projectile has an AEO, it will deal damage to all the targets within the splashranger.
    private float speed;//How many different enemies can it hit?
    private float lifeSpanRange;//How far can the projectile reach from the shooter?
    [SerializeField] private StatusEffect statusEffect;//The statusEffect to apply on the enemy.
    private float effectDuration;
    private float effectStrength;

    //For statistics
    //List<Enemy> enemiesHit = new List<Enemy>();

    private void Awake()
    {
        statusEffect = gameObject.GetComponent<StatusEffect>();//Could become more in future, change to array if it's the case
    }

    /// <summary>
    /// Set the correct data on the bullet, such as its damage and speed.
    /// </summary>
    /// <param name="_damage"></param>
    /// <param name="_pierces"></param>
    /// <param name="_splashRange"></param>
    /// <param name="_speed"></param>
    /// <param name="_lifeSpanRange"></param>
    /// <param name="_effectDuration"></param>
    /// <param name="_effectStrength"></param>
    public void SetProjectileData(float _damage, int _pierces, float _splashRange, float _speed, float _lifeSpanRange, float _effectDuration, float _effectStrength)
    {
        damage= _damage;
        pierces= _pierces;
        splashRange= _splashRange;
        speed= _speed;
        lifeSpanRange= _lifeSpanRange;
        effectDuration= _effectDuration;
        effectStrength= _effectStrength;
        if(statusEffect != null)
        {
            statusEffect.SetData(effectDuration, effectStrength);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.right * speed);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Hit(enemy);
            //Direct damage to the enemy that gets hit, splash damage on top!
            if (splashRange > 0)
            {
               Collider2D[] collidersWithinRange = Physics2D.OverlapCircleAll(this.transform.position,splashRange);
                foreach(Collider2D col in collidersWithinRange)
                {
                  if(col.TryGetComponent(out Enemy enemyInRange)) Hit(enemyInRange, true);//Does not consume a pierce
                }
            }
        }
    }


    void Update()
    {
        if ((tower.transform.position - transform.position).magnitude > lifeSpanRange) Die();
    }


    private void Hit(Enemy enemy, bool noPierce = false)
    {
        if (pierces <= 0 && !noPierce) return;
        //enemiesHit.Add(enemy);
        enemy.Damage(damage);
        if (statusEffect != null) enemy.ApplyStatusEffect(statusEffect);
        pierces--;
        if (pierces <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
