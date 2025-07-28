using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    protected float _damage;
    public float Damage => _damage;

    public abstract void Shoot(Vector3 direction);
}
