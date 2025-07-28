using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayCycle : IDisposable
{
    private MainHeroFactory _mainHeroFactory;

    private MainHeroConfig _mainHeroConfig;
    private Character _mainHero;

    private CounterService<EnemyCharacter> _counterService;

    private ConfirmPopup _confirmPopup;

    private LevelConfig _levelConfig;

    private EnemiesSpawner _enemiesSpawner;

    private MonoBehaviour _context;

    private GameMode _gameMode;

    public GameplayCycle(
        MainHeroFactory mainHeroFactory,
        MainHeroConfig mainHeroConfig,
        ConfirmPopup confirmPopup,
        CounterService<EnemyCharacter> enemiesCounter,
        LevelConfig levelConfig,
        EnemiesSpawner enemiesSpawner,
        MonoBehaviour context)
    {
        _confirmPopup = confirmPopup;
        _counterService = enemiesCounter;
        _mainHeroFactory = mainHeroFactory;
        _mainHeroConfig = mainHeroConfig;
        _levelConfig = levelConfig;
        _enemiesSpawner = enemiesSpawner;
        _context = context;
    }

    public IEnumerator Prepare()
    {
        yield return SceneManager.LoadSceneAsync("Environment", LoadSceneMode.Additive);

    }

    public IEnumerator Launch()
    {
        _confirmPopup.Show();
        _confirmPopup.ShowMessage($"Press {KeyCode.F.ToString()} for begin");

        yield return _confirmPopup.WaitConfirm(KeyCode.F);

        if(_mainHero == null)
            _mainHero = _mainHeroFactory.Create(_mainHeroConfig, _levelConfig.MainHeroStartPosition);

        _confirmPopup.Hide();

        _gameMode = new GameMode(_levelConfig, _mainHero, _enemiesSpawner);
        _gameMode.Start();

        _gameMode.Win += OnGameModeWin;
        _gameMode.Defeat += OnGameModeDefeat;
    }

    public void Update(float deltaTime) => _gameMode?.Update(deltaTime);

    private void OnGameModeEnded()
    {
        if (_gameMode != null)
        {
            _gameMode.Win -= OnGameModeWin;
            _gameMode.Defeat -= OnGameModeDefeat;
        }
    }

    public void Dispose()
    {
        OnGameModeEnded();
    }

    private void OnGameModeDefeat()
    {
        OnGameModeEnded();
        Debug.Log("Defeat");

        _context.StartCoroutine(Launch());
    }

    private void OnGameModeWin()
    {
        OnGameModeEnded();
        Debug.Log("Win");

        _context.StartCoroutine(Launch());
    }
}
