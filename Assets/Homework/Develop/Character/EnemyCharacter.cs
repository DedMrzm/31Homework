using UnityEngine;

public class EnemyCharacter : Character
{
    private float _damage;
    private CounterService<EnemyCharacter> _counterService;

    public void Initialize(float damage, CounterService<EnemyCharacter> counterService)
    {
        _damage = damage;
        _counterService = counterService;
    }

    private void OnDestroy()
    {
        _counterService?.Remove(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();

        if (collision.gameObject.GetComponent<IDamagable>() != null && collision.gameObject.CompareTag("MainHero"))
        {
            damagable.TakeDamage(_damage);
        }
    }
}
