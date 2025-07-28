using UnityEngine;

[RequireComponent (typeof(CapsuleCollider), typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    private float _damage;
    private float _speed;

    private Rigidbody _rigidbody;

    private Vector3 _shootDirection;

    private float _lifeTime = 1f;
    private float _timer = 0;

    public void Initialize(float damage, float speed, Vector3 shootDirection)
    {
        _rigidbody = GetComponent<Rigidbody>();

        _damage = damage;
        _shootDirection = shootDirection;
        _speed = speed;
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _lifeTime)
        {
            Destroy(gameObject);
            return;
        }
        _rigidbody.velocity = _shootDirection * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamagable>() != null)
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            damagable.TakeDamage(_damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
