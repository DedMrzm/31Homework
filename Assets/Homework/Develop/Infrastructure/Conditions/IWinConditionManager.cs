namespace Assets.Homework.Develop.Infrastructure
{
    public interface IWinConditionManager
    {
        void Update(float deltaTime);
        bool WinConditionCompleted();
    }
}
