using System;

namespace Assets.Homework.Develop.Infrastructure
{
    public class CaptureArenaLoseCondition : ILoseConditionManager
    {
        private CounterService<EnemyCharacter> _counterService;
        private int _countOfEnemiesForCaptureArena;

        public CaptureArenaLoseCondition(CounterService<EnemyCharacter> counterService, int countOfEnemiesForCaptureArena)
        {
            _counterService = counterService;
            _countOfEnemiesForCaptureArena = countOfEnemiesForCaptureArena;
        }

        public bool LoseConditionCompleted()
        {
            return _counterService.AddedCounter > _countOfEnemiesForCaptureArena;
        }

        public void Update(float deltaTime)
        {

        }
    }
}
