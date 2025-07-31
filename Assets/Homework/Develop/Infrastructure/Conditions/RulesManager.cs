using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Homework.Develop.Infrastructure
{
    public class RulesManager
    {
        private IWinConditionManager _winConditionManager;
        private ILoseConditionManager _loseConditionManager;

        private LevelConfig _levelConfig;
        private CounterService<EnemyCharacter> _counterService;
        private CharacterWithGun _mainHero;

        public RulesManager(LevelConfig levelConfig, CounterService<EnemyCharacter> counterService, CharacterWithGun mainHero)
        {
            _levelConfig = levelConfig;
            _counterService = counterService;
            _mainHero = mainHero;

            Debug.Log("RULES MANAGER");

            SetConditions(_levelConfig.WinCondition, _levelConfig.LoseCondition);
        }

        public IWinConditionManager WinConditionManager => _winConditionManager;
        public ILoseConditionManager LoseConditionManager => _loseConditionManager;

        public void Update(float deltaTime)
        {
            _winConditionManager.Update(deltaTime);
            _loseConditionManager.Update(deltaTime);
        }

        public void SetConditions(WinConditions winCondition, LoseConditions loseCondition)
        {
            switch (winCondition)
            {
                case WinConditions.SurviveNSeconds:
                    _winConditionManager = new SurviveNSecondsWinCondition(_levelConfig.TimeToSurviveForWin);
                    break;
                case WinConditions.KillNEnemies:
                    _winConditionManager = new KillNEnemiesWinCondition(_counterService, _levelConfig.CountOfKillEnemiesForWin);
                    break;
            }
            switch(loseCondition)
            {
                case LoseConditions.CaptureArena:
                    _loseConditionManager = new CaptureArenaLoseCondition(_counterService, _levelConfig.CountOfEnemiesForCaptureArena);
                    break;
                case LoseConditions.DeadHero:
                    _loseConditionManager = new DeadHeroLoseCondition(_mainHero);
                    break;
            }
        }
    }
}
