using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class CharactersFactory
{
    public Character CreateCharacter(
        Character prefab,
        float maxHealth,
        Vector3 spawnPosition,
        float moveSpeed,
        float rotationSpeed)
    {
        Character instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

        DirectionalMover mover;
        DirectionalRotator rotator;

        if(instance.TryGetComponent(out Rigidbody rigidbody))
        {
            mover = new RigidbodyDirectionalMover(rigidbody, moveSpeed);
            rotator = new RigidbodyDirectionalRotator(rigidbody, rotationSpeed);
        }
        else
        {
            throw new InvalidOperationException("Not found mover component");
        }

        instance.Initialize(mover, rotator, maxHealth);

        return instance;
    }

    public CharacterWithGun CreateCharacterWithGun(
        CharacterWithGun prefab,
        Vector3 spawnPosition,
        float maxHealth,
        float moveSpeed,
        float rotationSpeed)
    {
        CharacterWithGun instance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, null);

        DirectionalProjectileGunConfig gunConfig = Resources.Load<DirectionalProjectileGunConfig>("Configs/DirectionalProjectileGunConfig");

        DirectionalProjectileGun gun = instance.GetComponentInChildren<DirectionalProjectileGun>();

        gun.Initialize(gunConfig.Damage, gunConfig.ProjectileSpeed, gunConfig.ProjectilePrefab);

        DirectionalMover mover;
        DirectionalRotator rotator;

        if (instance.TryGetComponent(out Rigidbody rigidbody))
        {
            mover = new RigidbodyDirectionalMover(rigidbody, moveSpeed);
            rotator = new RigidbodyDirectionalRotator(rigidbody, rotationSpeed);
        }
        else
        {
            throw new InvalidOperationException("Not found mover component");
        }

        instance.Initialize(mover, rotator, maxHealth, gun);

        return instance;
    }
}
