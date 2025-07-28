using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemiesSpawner
{
    private EnemiesFactory _enemiesFactory;
    private CounterService<EnemyCharacter> _counterService;

    private MonoBehaviour _context;

    private float _cooldown = 5f;

    private List<Transform> _spawnPoints;

    public CounterService<EnemyCharacter> CounterService => _counterService;

    public EnemiesSpawner(EnemiesFactory enemiesFactory, CounterService<EnemyCharacter> counterService, MonoBehaviour context, List<Transform> spawnPoints)
    {
        _enemiesFactory = enemiesFactory;
        _counterService = counterService;
        _context = context;

        _spawnPoints = spawnPoints;
    }

    public IEnumerator SpawnProcess(
        EnemyConfig enemyConfig)
   {
        while (true)
        {
            foreach (Transform spawnPoint in _spawnPoints)
            {
                EnemyCharacter enemyCharacter = _enemiesFactory.CreateEnemy(enemyConfig, spawnPoint.position);
                _counterService.Add(enemyCharacter);
            }

            yield return new WaitForSeconds(_cooldown);
        }

    }
}
