using Assets.Homework.Develop.Infrastructure;
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

    private RulesManager _rulesManager;

    private bool _isRunning;

    public GameMode(
        LevelConfig levelConfig,
        CharacterWithGun mainHero,
        EnemiesSpawner enemiesSpawner,
        RulesManager rulesManager)
    {
        _levelConfig = levelConfig;
        _mainHero = mainHero;
        _enemiesSpawner = enemiesSpawner;

        _rulesManager = rulesManager;
    }

    public void Start()
    {
        _enemiesSpawner.CounterService.Restart();

        _mainSpawnProcess = _mainHero.StartCoroutine(_enemiesSpawner.SpawnProcess(_levelConfig.EnemyConfig));

        _isRunning = true;
    }

    public void Update(float deltaTime)
    {
        if (_isRunning == false)
            return;

        _rulesManager.Update(deltaTime);

        if (_rulesManager.LoseConditionManager.LoseConditionCompleted())
        {
            ProcessDefeat();
            return;
        }

        if (_rulesManager.WinConditionManager.WinConditionCompleted())
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
        Debug.Log("LOSE");
        ProcessEndGame();
        Defeat?.Invoke();
    }

    private void ProcessWin()
    {
        Debug.Log("WINWIN");
        ProcessEndGame();
        Win?.Invoke();
    }

}
