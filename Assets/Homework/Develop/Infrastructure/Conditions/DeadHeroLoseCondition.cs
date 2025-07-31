namespace Assets.Homework.Develop.Infrastructure
{
    public class DeadHeroLoseCondition : ILoseConditionManager
    {
        private CharacterWithGun _mainHero;

        public DeadHeroLoseCondition(CharacterWithGun mainHero)
        {
            _mainHero = mainHero;
        }

        public bool LoseConditionCompleted()
        {
            return _mainHero.Health.Value <= 0;
        }

        public void Update(float deltaTime)
        {
            
        }
    }
}
