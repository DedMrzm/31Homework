using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithGun : Character
{
    [SerializeField] private Transform _cameraTarget;
    
    private const KeyCode ShootCode = KeyCode.Mouse0;

    private DirectionalProjectileGun _gun;

    public Transform CameraTarget => _cameraTarget;

    public void Initialize(DirectionalMover mover, DirectionalRotator rotator, float maxHealth, DirectionalProjectileGun gun)
    {
        _gun = gun;

        base.Initialize(mover, rotator, maxHealth);
    }

    public override void Update()
    {
        if (IsInit == false)
            return;

        base.Update();

        if (Input.GetKeyDown(ShootCode))
        {
            Shoot(_gun.transform.forward);
        }
    }

    private void OnDestroy()
    {
        Destroy(CameraTarget.gameObject);
    }

    public void Shoot(Vector3 direction)
        => _gun.Shoot(direction);
}
