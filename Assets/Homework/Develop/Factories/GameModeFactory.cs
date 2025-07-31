using Assets.Homework.Develop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Homework.Develop.Factories
{
    public class GameModeFactory
    {
        public GameMode CreateGameMode(
        LevelConfig levelConfig,
        CharacterWithGun mainHero,
        CounterService<EnemyCharacter> counterService,
        EnemiesSpawner enemiesSpawner)
        {
            RulesManager rulesManage = new RulesManager(levelConfig, counterService, mainHero);

            return new GameMode(levelConfig, mainHero, enemiesSpawner, rulesManage);
        }
    }
}
