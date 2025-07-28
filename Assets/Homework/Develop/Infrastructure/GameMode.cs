using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class GameMode
{
    public event Action Win;
    public event Action Defeat;

    private LevelConfig _levelConfig;

    private float _timer = 0;

    private Character _mainHero;

    private Coroutine _mainSpawnProcess;

    private EnemiesSpawner _enemiesSpawner;

    private bool _isRunning;

    public GameMode(
        LevelConfig levelConfig,
        Character mainHero,
        EnemiesSpawner enemiesSpawner)
    {
        _levelConfig = levelConfig;
        _mainHero = mainHero;
        _enemiesSpawner = enemiesSpawner;
    }

    public void Start()
    {
        //_currentTimeToDefeat = _levelConfig.TimeToDefeat;
        //_currentDistanceTraveled = 0;
        _enemiesSpawner.CounterService.Restart();

        _mainSpawnProcess = _mainHero.StartCoroutine(_enemiesSpawner.SpawnProcess(_levelConfig.EnemyConfig));

        _isRunning = true;
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        _timer += deltaTime;

        if (DefeatConditionCompleted())
        {
            ProcessDefeat();
            return;
        }

        if (WinConditionCompleted())
        {
            ProcessWin();
            return;
        }
    }

    private void ProcessEndGame()
    {
        _isRunning = false;

        foreach (EnemyCharacter enemy in _enemiesSpawner.CounterService.Items)
            enemy.Destroy();

        _mainHero.StopCoroutine(_mainSpawnProcess);
        _mainHero = null;
    }

    private void ProcessDefeat()
    {
        ProcessEndGame();
        Defeat?.Invoke();
    }

    private void ProcessWin()
    {
        Debug.Log("WINWIN");
        ProcessEndGame();
        Win?.Invoke();
    }

    private bool WinConditionCompleted()
    {
        switch(_levelConfig.WinCondition)
        {
            case WinConditions.SurviveNSeconds:
                if (_timer > _levelConfig.TimeToSurviveForWin)
                {
                    _timer = 0;
                    return true;
                }
                break;
            case WinConditions.KillNEnemies:
                if(_enemiesSpawner.CounterService.RemovedCounter >= _levelConfig.CountOfKillEnemiesForWin)
                {
                    return true;
                }
                break;
        }
        return false;
    }
    private bool DefeatConditionCompleted()
    {
        switch (_levelConfig.LoseCondition)
        {
            case LoseConditions.CaptureArena:
                if (_enemiesSpawner.CounterService.AddedCounter > _levelConfig.CountOfEnemiesForCaptureArena)
                {
                    return true;
                }
                break;
            case LoseConditions.DeadHero:
                if (_mainHero.Health.Value <= 0)
                    return true;
                break;
        }
        return false;
    }
}
