using System;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine;

public class Health
{
    public event Action Reduced;
    public event Action Added;

    private float _maxValue;

    private float _value;

    public Health(float maxHealth)
    {
        _maxValue = maxHealth;
        _value = maxHealth;
    }

    public float Value => _value;

    public void Reduce(float reducedValue)
    {
        if (reducedValue < 0)
        {
            Debug.LogError(nameof(reducedValue));
            return;
        }

        _value = Mathf.Clamp(_value - reducedValue, 0, _maxValue);


        Reduced?.Invoke();
    }

    public void Add(float additiveValue)
    {
        if (additiveValue < 0)
        {
            Debug.LogError(nameof(additiveValue));
            return;
        }

        _value = Mathf.Clamp(_value + additiveValue, 0, _maxValue);

        Added?.Invoke();
    }
}
