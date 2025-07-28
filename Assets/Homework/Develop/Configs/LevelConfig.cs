using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
public class LevelConfig : ScriptableObject
{
    [field: SerializeField] public Vector3 MainHeroStartPosition { get; private set; }

    [field: SerializeField] public WinConditions WinCondition { get; private set; }
    [field: SerializeField] public float TimeToSurviveForWin { get; private set; }
    [field: SerializeField] public float CountOfKillEnemiesForWin { get; private set; }

    [field: SerializeField] public LoseConditions LoseCondition { get; private set; }
    [field: SerializeField] public float CountOfEnemiesForCaptureArena { get; private set; }

    [field: SerializeField] public EnemyConfig EnemyConfig { get; private set; }
    [field: SerializeField] public int EnemiesSpawnCooldown { get; private set; }
    [field: SerializeField] public Transform EnemiesSpawnPoints { get; private set; }


    [ContextMenu("UpdateStartHeroPosition")]
    private void UpdateStartHeroPosition()
    {
        GameObject point = GameObject.FindGameObjectWithTag("StartHeroPosition");
        MainHeroStartPosition = point.transform.position;
    }
}
