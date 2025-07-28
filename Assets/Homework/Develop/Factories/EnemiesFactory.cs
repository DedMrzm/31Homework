using UnityEngine;

public class EnemiesFactory
{
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;
    private CounterService<EnemyCharacter> _counterService;

    public EnemiesFactory(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory,
        CounterService<EnemyCharacter> counterService)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
        _counterService = counterService;
    }

    public EnemyCharacter CreateEnemy(
        EnemyConfig config,
        Vector3 spawnPosition)
    {
        EnemyCharacter instance = (EnemyCharacter) _charactersFactory.CreateCharacter(
                config.Prefab,
                config.MaxHealth,
                spawnPosition,
                config.MoveSpeed,
                config.RotationSpeed);

        instance.Initialize(config.Damage, _counterService);

        Controller controller = _controllersFactory.CreateEnemyController(
            instance);

        controller.Enable();

        _controllersUpdateService.Add(controller, () => instance.IsDestroyed);

        return instance;
    }
}
