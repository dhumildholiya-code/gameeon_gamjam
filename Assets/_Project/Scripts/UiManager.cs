using GamJam.GameStates;
using UnityEngine;

namespace GamJam
{
    [System.Serializable]
    public sealed class UiManager
    {
        public MainMenuUi mainMenu;
        public GameplayUi gameplay;
        public LevelCompleteUi levelComplete;
        public LevelFailedUi levelFailed;
        public LevelSelectUi levelSelect;
        public GameObject gameComplete;
        public GameButton quitButton;

        [SerializeField]
        private GameObject _loadingScreen;

        private GameManager _gameManager;
        public string LevelName => _gameManager.LevelName;

        public void Init(GameManager gameManager)
        {
            _gameManager = gameManager;
            mainMenu?.Init(this);
            gameplay?.Init(this);
            levelComplete?.Init(this);
            levelFailed?.Init(this);
            levelSelect?.Init(this, _gameManager.Levels);
            quitButton.onClick.AddListener(QuitButton);
        }
        public void Exit()
        {
            mainMenu?.Exit();
            gameplay?.Exit();
            levelComplete?.Exit();
            levelFailed?.Exit();
            levelSelect?.Exit();
        }

        public void ShowLoadingScreen(bool isActive)
        {
            _loadingScreen.SetActive(isActive);
        }

        public void PlayButton()
        {
            _gameManager.RetryLevelAsync();
        }
        public void OptionsButton()
        {
        }
        public void NextLevel(int levelId = -1)
        {
            _gameManager.LoadLevelAsync(levelId);
        }
        public void Retry()
        {
            _gameManager.RetryLevelAsync();
        }
        public void GoToMainMenu()
        {
            _gameManager.ChangeState(GameState.MainMenu);
        }
        public void QuitButton()
        {
            _gameManager.ChangeState(GameState.Quit);
        }
    }
}
