using System;

namespace Assets.Homework.Develop.Infrastructure
{
    public class KillNEnemiesWinCondition : IWinConditionManager
    {
        private CounterService<EnemyCharacter> _counterService;
        private int _countOfKillEnemiesForWin;

        public KillNEnemiesWinCondition(CounterService<EnemyCharacter> counterService, int countOfKillEnemiesForWin)
        {
            _counterService = counterService;
            _countOfKillEnemiesForWin = countOfKillEnemiesForWin;
        }

        public void Update(float deltaTime)
        {

        }

        public bool WinConditionCompleted()
        {
            return _counterService.RemovedCounter >= _countOfKillEnemiesForWin;
        }
    }
}
