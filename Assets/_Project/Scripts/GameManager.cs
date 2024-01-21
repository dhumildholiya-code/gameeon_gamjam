using GamJam.EventChannel;
using GamJam.GameStates;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamJam
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static event Action<int> OnLevelUnlocked;

        [SerializeField]
        private GameState _startState;
        [SerializeField]
        private int _startLevel;
        [SerializeField]
        private LevelData[] _levels;
        [Header("Events to Listen")]
        public GameStateEventChannel gameStateChange;
        [SerializeField]
        private UiManager _uiManager;

        private BaseGameState _currentState;
        private MainMenuState _mainMenuState;
        private GameplayState _gameplayState;
        private LevelCompleteState _levelCompleteState;
        private LevelFailedState _levelFailedState;
        private GameCompleteState _gameCompleteState;

        private int _currentLevelId;
        private int _lastLevelUnlocked;
        public LevelData[] Levels => _levels;
        public string LevelName => $"{_levels[_currentLevelId].levelName}";
        public string CurrentSceneName => $"Level_{_currentLevelId + 1}";
        public UiManager Ui => _uiManager;

        public GameController GameController { get; set; }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            _lastLevelUnlocked = PlayerPrefs.GetInt("lastLevelUnlocked", 1);
            for (int i = 0; i < _lastLevelUnlocked; i++)
            {
                _levels[i].isLocked = false;
            }

            _uiManager.Init(this);
            _mainMenuState = new MainMenuState(this);
            _gameplayState = new GameplayState(this);
            _levelCompleteState = new LevelCompleteState(this);
            _levelFailedState = new LevelFailedState(this);
            _gameCompleteState = new GameCompleteState(this);


            _currentLevelId = _startLevel;
            ChangeState(_startState);
        }

        public void ChangeState(GameState newState)
        {
            _currentState?.Exit();

            switch (newState)
            {
                case GameState.MainMenu:
                    _currentState = _mainMenuState;
                    break;
                case GameState.Gameplay:
                    _currentState = _gameplayState;
                    break;
                case GameState.LevelComplete:
                    _currentState = _levelCompleteState;
                    break;
                case GameState.LevelFailed:
                    _currentState = _levelFailedState;
                    break;
                case GameState.Quit:
                    Application.Quit();
                    break;
                case GameState.GameComplete:
                    _currentState = _gameCompleteState;
                    break;
            }
            _currentState.Enter();
        }
        public void SetLevelComplete()
        {
            if (_currentLevelId + 1 >= _levels.Length)
            {
                ChangeState(GameState.GameComplete);
                return;
            }
            _levels[_currentLevelId + 1].isLocked = false;
            OnLevelUnlocked?.Invoke(_currentLevelId + 1);
            PlayerPrefs.SetInt("lastLevelUnlocked", _currentLevelId + 1);
        }

        public async void LoadLevelAsync(int levelIndex = -1)
        {
            if (levelIndex != -1)
                _currentLevelId = levelIndex;
            else
                _currentLevelId++;
            Ui.ShowLoadingScreen(true);
            await LoadScene(CurrentSceneName);
            await Task.Delay(100);
            Ui.ShowLoadingScreen(false);
            ChangeState(GameState.Gameplay);
        }
        public async void RetryLevelAsync()
        {
            Ui.ShowLoadingScreen(true);
            await LoadScene(CurrentSceneName);
            await Task.Delay(100);
            Ui.ShowLoadingScreen(false);
            ChangeState(GameState.Gameplay);
        }
        public async Task LoadScene(string level)
        {
            var ao = SceneManager.LoadSceneAsync(level);
            while (ao.isDone)
            {
                await Task.Yield();
            }
        }
    }
}
