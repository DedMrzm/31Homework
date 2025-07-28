using System;
using Unity.VisualScripting;
using UnityEngine;

public class DamagableView : MonoBehaviour, IInitializable
{
    private readonly int TakeDamageKey = Animator.StringToHash("TakeDamage");
    private readonly int IsDeadKey = Animator.StringToHash("IsDead");

    [SerializeField] private Animator _animator;

    [SerializeField] private ParticleSystem _hitParticles;

    private bool _isDead = false;

    private IDamagable _damagable;

    public void Initialize()
    {
        _damagable = GetComponentInParent<IDamagable>();

        _damagable.Health.Reduced += OnTakeDamage;
    }

    public void OnDestroy()
    {
        _damagable.Health.Reduced -= OnTakeDamage;
    }

    private void OnTakeDamage()
    {
        if(_isDead) 
            return;

        if(_hitParticles != null)
            _hitParticles.Play();

        _animator.SetTrigger(TakeDamageKey);

        if (_damagable.Health.Value <= 0)
        {
            _animator.transform.parent.gameObject.GetComponent<Collider>().enabled = false;

            _animator.SetBool(IsDeadKey, true);

            Destroy(gameObject.transform.parent.gameObject, 1.5f);

            _isDead = true;
        }
    }

}
