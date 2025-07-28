using System;

public interface IDamagable
{
    Health Health { get; }
    void TakeDamage(float damage);
}
