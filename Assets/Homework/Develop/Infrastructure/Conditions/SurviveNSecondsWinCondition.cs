namespace Assets.Homework.Develop.Infrastructure
{
    public class SurviveNSecondsWinCondition : IWinConditionManager
    {
        private float _timer = 0;
        private float _timeToSurviveForWin;

        public SurviveNSecondsWinCondition(float timeToSurviveForWin)
        {
            _timeToSurviveForWin = timeToSurviveForWin;
        }

        public bool WinConditionCompleted()
        {
            if(_timer > _timeToSurviveForWin)
            {
                _timer = 0;
                return true;
            }
            else
                return false;
        }

        public void Update(float deltaTime)
        {
            if (WinConditionCompleted())
                return;

            _timer += deltaTime;
        }
    }
}
