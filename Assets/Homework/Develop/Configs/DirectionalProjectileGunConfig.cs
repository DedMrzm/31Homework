using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/DirectionalProjectileGunConfig", fileName = "DirectionalProjectileGunConfig")]
public class DirectionalProjectileGunConfig : ScriptableObject
{
    [field: SerializeField] public Projectile ProjectilePrefab;
    [field: SerializeField] public float Damage;
    [field: SerializeField] public float ProjectileSpeed;
}
