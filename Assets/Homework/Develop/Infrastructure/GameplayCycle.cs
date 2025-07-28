//using System;
//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class GameplayCycle : IDisposable
//{
//    private MainHeroFactory _mainHeroFactory;

//    private MainHeroConfig _mainHeroConfig;
//    private Character _mainHero;

//    private LevelConfig _levelConfig;

//    //private ConfirmPopup _confirmPopup;

//    //private EnemiesSpawner _enemiesSpawner;

//    private MonoBehaviour _context;

//    private GameMode _gameMode;

//    public GameplayCycle(
//        MainHeroFactory mainHeroFactory,
//        MainHeroConfig mainHeroConfig,
//        LevelConfig levelConfig,
//        //ConfirmPopup confirmPopup,
//        //EnemiesSpawner enemiesSpawner,
//        MonoBehaviour context)
//    {
//        _mainHeroFactory = mainHeroFactory;
//        _mainHeroConfig = mainHeroConfig;
//        _levelConfig = levelConfig;
//        //_confirmPopup = confirmPopup;
//        //_enemiesSpawner = enemiesSpawner;
//        _context = context;
//    }

//    public IEnumerator Prepare()
//    {
//        yield return SceneManager.LoadSceneAsync(_levelConfig.EnviromentSceneName, LoadSceneMode.Additive);

//        _mainHero = _mainHeroFactory.Create(_mainHeroConfig, _levelConfig.MainHeroStartPosition);
//    }

//    public IEnumerator Launch()
//    {
//        _confirmPopup.Show();
//        _confirmPopup.ShowMessage($"Press {KeyCode.F.ToString()} for begin");

//        yield return _confirmPopup.WaitConfirm(KeyCode.F);

//        _confirmPopup.Hide();

//        _gameMode = new GameMode(_levelConfig, _mainHero, _enemiesSpawner);

//        _gameMode.Win += OnGameModeWin;
//        _gameMode.Defeat += OnGameModeDefeat;

//        _gameMode.Start();
//    }

//    public void Update(float deltaTime) => _gameMode?.Update(deltaTime);

//    private void OnGameModeEnded()
//    {
//        if (_gameMode != null)
//        {
//            _gameMode.Win -= OnGameModeWin;
//            _gameMode.Defeat -= OnGameModeDefeat;
//        }
//    }

//    public void Dispose()
//    {
//        OnGameModeEnded();
//    }

//    private void OnGameModeDefeat()
//    {
//        OnGameModeEnded();
//        Debug.Log("Defeat");
//        _context.StartCoroutine(Launch());
//    }

//    private void OnGameModeWin()
//    {
//        OnGameModeEnded();
//        Debug.Log("Win");
//        SceneManager.LoadScene("Menu");
//    }
//}
