using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    Tower tower;
    [SerializeField] Enemy target;

    [SerializeField] GameObject projectilePrefab;//The projectile that will be shot
    [SerializeField] private float projectileSpeed;//How fast does the projectile fly?
    [SerializeField] private float shootSpeed;//How fast can it shoot?
    [SerializeField] private float shootRange;//How far do the projectiles go?
    bool isShooting = false;
    private float _shootRange;//How far do the projectiles actually go including the range of the tower?

    //[SerializeField] float rotationSpeed; //How fast can it rotate?

    private void Awake()
    {
        tower = GetComponentInParent<Tower>();

    }

    private void Start()
    {
        _shootRange = shootRange + (transform.parent.GetComponentInChildren<Range>().GetRange() / 2);//Divided by 2 as it's a radius
    }

    void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();
        projectile.tower = tower;
        projectile.speed = projectileSpeed;
        projectile.lifeSpanRange = _shootRange;
    }

    private void Update()
    {
        if (target != null)//If a target is set
        {
            LookAtTarget(); //Follow the target
            if (!isShooting) { StartCoroutine(ShootEnum()); }// If we aren't shooting yet, start the shooting sequence
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
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
