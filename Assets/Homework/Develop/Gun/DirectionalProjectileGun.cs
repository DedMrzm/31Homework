using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalProjectileGun : Gun
{
    private float _projectileSpeed;

    private Projectile _projectile;

    [SerializeField] private Transform _shootPoint;

    private bool _isInit = false;

    public void Initialize(float damage, float projectileSpeed, Projectile projectile)
    {
        _damage = damage;
        _projectileSpeed = projectileSpeed;
        _projectile = projectile;

        _isInit = true;
    }

    public override void Shoot(Vector3 shootDirection)
    {
        if(_isInit)
        {
            Debug.Log("GUN SHOOTED");
            Projectile projectile = Instantiate(_projectile, _shootPoint.position, Quaternion.LookRotation(shootDirection), _shootPoint);

            projectile.Initialize(Damage, _projectileSpeed, shootDirection);
        }
    }
}
    
