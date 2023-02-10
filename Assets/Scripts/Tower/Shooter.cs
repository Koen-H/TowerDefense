using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The shooter is on top of a tower, there can be multiple shooters.
/// The shooter shoots projectiles
/// The shooter has all the variables stored for the projectile, they are changed based on upgrades.
/// </summary>
public class Shooter : MonoBehaviour
{
    Tower tower;
    Enemy target;
    private float shootSpeed;//How fast can it shoot?
    private float projectileRange;//How far do the projectiles go?
    bool isShooting = false;
    Range range;

    private GameObject projectilePrefab;//The projectile that will be shot
    private float projectileDamage;//
    private int projectilePierces;//
    private float splashrange;
    private float projectileSpeed;//How fast does the projectile fly?
    private float lifeSpanRange;//How far do the projectiles actually go including the range of the tower?
    private float projectileEffectDuration;
    private float projectileEffectStrength;

    //[SerializeField] float rotationSpeed; //How fast can it rotate?

    private void Awake()
    {
        tower = GetComponentInParent<Tower>();
        range = transform.parent.GetComponentInChildren<Range>();
    }

    private void Start()
    {
       CalculateProjectileRange();
    }

    /// <summary>
    /// Only happens once at start, then gets overwritten by the upgrades
    /// </summary>
    /// <param name="_projectilePrefab"></param>
    /// <param name="_projectileDamage"></param>
    /// <param name="_projectilePierces"></param>
    /// <param name="_splashrange"></param>
    /// <param name="_projectileSpeed"></param>
    /// <param name="_projectileRange"></param>
    /// <param name="_projectileEffectDuration"></param>
    /// <param name="_projectileEffectStrength"></param>
    /// /// <param name="_shootSpeed"></param>
    public void SetData(GameObject _projectilePrefab,int _projectileDamage, int _projectilePierces,float _splashrange, float _projectileSpeed, float _projectileRange, float _projectileEffectDuration, float _projectileEffectStrength, float _shootSpeed)
    {
        projectilePrefab = _projectilePrefab;
        projectileDamage = _projectileDamage;
        projectilePierces = _projectilePierces;
        splashrange = _splashrange;
        projectileSpeed = _projectileSpeed;
        projectileRange = _projectileRange;
        projectileEffectDuration = _projectileEffectDuration;
        projectileEffectStrength = _projectileEffectStrength;
        shootSpeed = _shootSpeed;


    }
    public void CalculateProjectileRange()
    {
        lifeSpanRange = projectileRange + (range.GetRange() / 2);//Divided by 2 as it's a radius
    }

    void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();
        projectile.tower = tower;
        projectile.SetProjectileData(projectileDamage, projectilePierces, splashrange, projectileSpeed, lifeSpanRange, projectileEffectDuration, projectileEffectStrength);
    }

    private void Update()
    {
        if (target != null)//If a target is set
        {
            LookAtTarget(); //Follow the target
            if (!isShooting) { StartCoroutine(ShootEnum()); }// If we aren't shooting yet, start the shooting sequence
        }

    }

    public void SetTarget(Enemy newTarget)
    {
        target = newTarget;
    }

    public void SetSpeed(float speed)
    {
        shootSpeed = speed;
    }
    public void SetDamage(float damage)
    {
        projectileDamage = damage;
    }
    public void SetEffectStrength(float newEffectStrength)
    {
        projectileEffectStrength = newEffectStrength;
    }
    public void SetEffectDuration(float newEffectDuration)
    {
        projectileEffectDuration = newEffectDuration;
    }
    public void SetAOE(float newSplashrange)
    {
        splashrange = newSplashrange;
    }

    void LookAtTarget()
    {
        transform.right = target.transform.position - transform.position;
        //For fancy rotation
        //float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        //Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    IEnumerator ShootEnum()
    {
        isShooting = true;
        while (target != null)
        {
            Shoot();
            yield return new WaitForSeconds(shootSpeed);
        }
        isShooting = false;
    }

}
