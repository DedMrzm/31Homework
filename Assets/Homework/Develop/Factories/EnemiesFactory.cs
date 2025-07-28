using UnityEngine;

public class EnemiesFactory
{
    private ControllersUpdateService _controllersUpdateService;
    private ControllersFactory _controllersFactory;
    private CharactersFactory _charactersFactory;

    public EnemiesFactory(
        ControllersUpdateService controllersUpdateService,
        ControllersFactory controllersFactory,
        CharactersFactory charactersFactory)
    {
        _controllersUpdateService = controllersUpdateService;
        _controllersFactory = controllersFactory;
        _charactersFactory = charactersFactory;
    }

    public Character CreateEnemy(
        EnemyConfig config,
        Vector3 spawnPosition)
    {
        Character instance = _charactersFactory.CreateCharacter(
                config.Prefab,
                config.MaxHealth,
                spawnPosition,
                config.MoveSpeed,
                config.RotationSpeed);

        Controller controller = _controllersFactory.CreateAlongMovableVelocityRotatableController(
            instance,
            instance);

        controller.Enable();

        _controllersUpdateService.Add(controller, () => instance.IsDestroyed);

        return instance;
    }
}
