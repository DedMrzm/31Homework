using Assets.Homework.Develop.Factories;
using Assets.Homework.Develop.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private ConfirmPopup _confirmPopup;

    private CounterService<EnemyCharacter> _counterService;

    private ControllersUpdateService _controllersUpdateService;

    private GameplayCycle _gameplayCycle;

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        MainHeroConfig heroConfig = Resources.Load<MainHeroConfig>("Configs/MainHeroConfig");
        LevelConfig levelConfig = Resources.Load<LevelConfig>("Configs/LevelConfig");

        _controllersUpdateService = new ControllersUpdateService();

        CounterService<EnemyCharacter> counterService = new CounterService<EnemyCharacter>();


        ControllersFactory controllersFactory = new ControllersFactory();
        CharactersFactory charactersFactory = new CharactersFactory();

        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, controllersFactory, charactersFactory);
        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, controllersFactory, charactersFactory, counterService);


        _counterService = counterService;

        List<Transform> spawnPoints = levelConfig.EnemiesSpawnPoints.GetComponentsInChildren<Transform>().ToList();
        spawnPoints.Remove(spawnPoints[0]);
        EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory, counterService, this, spawnPoints);

        GameModeFactory gameModeFactory = new GameModeFactory();

        _gameplayCycle = new GameplayCycle(
            mainHeroFactory,
            heroConfig,
            _confirmPopup,
            counterService,
            levelConfig,
            enemiesSpawner,
            this,
            gameModeFactory);

        yield return _gameplayCycle.Prepare();

        yield return new WaitForSeconds(1.5f);

        yield return _gameplayCycle.Launch();
    }

    private void OnDestroy()
    {
        _gameplayCycle?.Dispose();
    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);

        _gameplayCycle?.Update(Time.deltaTime);
    }
}
