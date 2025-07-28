using UnityEngine;
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
}
