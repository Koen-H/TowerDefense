using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    Tower tower;
    [SerializeField] GameObject projectilePrefab;//The projectile that will be shot
    [SerializeField] private float projectileSpeed;//How fast does the projectile fly?
    [SerializeField] private float shootSpeed;//How fast can it shoot?
    [SerializeField] private float shootRange;//How far do the projectiles go?
    [SerializeField] private float _shootRange;//How far do the projectiles actually go including the range of the tower?

    private void Awake()
    {
        tower = GetComponentInParent<Tower>();

    }

    private void Start()
    {
        _shootRange = shootRange + (transform.parent.GetComponentInChildren<Range>().GetRange()/2);//Divided by 2 as it's a radius
    }

    void Shoot()
    {
        Projectile projectile = Instantiate(projectilePrefab, transform.position, transform.rotation).GetComponent<Projectile>();
        projectile.tower= tower;
        projectile.speed = projectileSpeed;
        projectile.lifeSpanRange = _shootRange;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Shoot();
        }
    }
}
