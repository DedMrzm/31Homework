public class ControllersFactory
{
    public PlayerDirectionalMovableController CreatePlayerDirectionalMovableController(IDirectionalMovable movable)
    {
        return new PlayerDirectionalMovableController(movable);
    }

    public AlongMovableVelocityRotatableController CreateAlongMovableVelocityRotatableController(
        IDirectionalMovable movable,
        IDirectionalRotatable rotatable)
    {
        return new AlongMovableVelocityRotatableController(rotatable, movable);
    }

    public CompositeController CreateMainHeroPlayerController(CharacterWithGun character)
    {
        return new CompositeController(
            CreatePlayerDirectionalMovableController(character),
            CreateAlongMovableVelocityRotatableController(character, character));
    }

    public RandomDirectionalMovableController CreateRandomDirectionalMovableController(EnemyCharacter character, float timeToChangeDirection = 1f)
    {
        return new RandomDirectionalMovableController(character, timeToChangeDirection);
    }

    public CompositeController CreateEnemyController(EnemyCharacter enemy)
    {
        return new CompositeController(
            CreateRandomDirectionalMovableController(enemy),
            CreateAlongMovableVelocityRotatableController(enemy, enemy));
    }
}
