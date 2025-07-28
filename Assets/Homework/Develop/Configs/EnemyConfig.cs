using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Gameplay/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
    [field: SerializeField] public Character Prefab;
    [field: SerializeField] public float MaxHealth { get; private set; } = 10f;
    [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
    [field: SerializeField] public float RotationSpeed { get; private set; } = 900f;
    [field: SerializeField] public float TimeToSpawn { get; private set; } = 0.5f;
    [field: SerializeField] public float Damage { get; private set; } = 1f;
}
