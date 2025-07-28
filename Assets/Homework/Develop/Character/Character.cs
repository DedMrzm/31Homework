using System;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoDestroyable, IDirectionalMovable, IDirectionalRotatable, IDamagable
{
    private DirectionalMover _mover;
    private DirectionalRotator _rotator;

    private Health _health;

    protected bool IsInit = false;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => transform.position;

    public Health Health => _health;

    public void Initialize(DirectionalMover mover, DirectionalRotator rotator, float maxHealth)
    {
        _mover = mover;
        _rotator = rotator;

        _health = new Health(maxHealth);

        foreach (IInitializable initializable in GetComponentsInChildren<IInitializable>())
            initializable.Initialize();

        IsInit = true;
    }

    public virtual void Update()
    {
        if (IsInit == false)
            return;

        if(_health.Value <= 0)
            return;

        _mover.Update(Time.deltaTime);
        _rotator.Update(Time.deltaTime);
    }

    public void SetMoveDirection(Vector3 inputDirection) => _mover.SetInputDirection(inputDirection);

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public void TakeDamage(float damage)
        => _health.Reduce(damage);
}
