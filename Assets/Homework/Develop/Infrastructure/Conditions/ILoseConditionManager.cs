namespace Assets.Homework.Develop.Infrastructure
{
    public interface ILoseConditionManager
    {
        public void Update(float deltaTime);
        public bool LoseConditionCompleted();
    }
}
