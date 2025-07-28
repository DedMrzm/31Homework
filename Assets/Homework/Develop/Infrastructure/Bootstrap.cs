using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private ControllersUpdateService _controllersUpdateService;

    private MainHeroFactory _heroFactory;

    private void Awake()
    {
        StartCoroutine(StartProcess());
    }

    private IEnumerator StartProcess()
    {
        ControllersFactory controllersFactory = new ControllersFactory();
        CharactersFactory charactersFactory = new CharactersFactory();

        MainHeroConfig heroConfig = Resources.Load<MainHeroConfig>("Configs/MainHeroConfig");
        LevelConfig levelConfig = Resources.Load<LevelConfig>("Configs/LevelConfig");

        _controllersUpdateService = new ControllersUpdateService();

        MainHeroFactory mainHeroFactory = new MainHeroFactory(_controllersUpdateService, controllersFactory, charactersFactory);
        EnemiesFactory enemiesFactory = new EnemiesFactory(_controllersUpdateService, controllersFactory, charactersFactory);


        //EnemiesSpawner enemiesSpawner = new EnemiesSpawner(enemiesFactory);

        
        mainHeroFactory.Create(heroConfig, levelConfig.MainHeroStartPosition);

        //_gameplayCycle = new GameplayCycle(
        //    mainHeroFactory,
        //    heroConfig,
        //    levelConfig,
        //    _confirmPopup,
        //    enemiesSpawner,
        //    this);

        //Сделать мейнхиро фектори
        //В нём должна быть реализация создания всех необходимых классов, а также поиск гана в чилдрене и про его отсутсвии что-то сделать


        yield return new WaitForSeconds(1);

        SceneManager.LoadScene("Environment", LoadSceneMode.Additive);

    }

    private void Update()
    {
        _controllersUpdateService?.Update(Time.deltaTime);
    }
}
