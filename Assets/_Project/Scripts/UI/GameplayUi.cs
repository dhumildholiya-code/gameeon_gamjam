using TMPro;
using UnityEngine;

namespace GamJam
{
    public class GameplayUi : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _levelIndicator;
        [SerializeField]
        private GameObject _pauseMenu;
        [SerializeField]
        private MenuButton _restart;
        [SerializeField]
        private MenuButton _mainMenu;

        private UiManager _uiManager;
        private bool _isPause;

        public void Init(UiManager uiManager)
        {
            _isPause = false;
            _uiManager = uiManager;
        }
        public void Exit()
        {
            _uiManager = null;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _isPause = !_isPause;
                _pauseMenu.SetActive(_isPause);
            }
        }

        public void Show(bool isActive)
        {
            if (isActive)
            {
                _restart.onClick.AddListener(_uiManager.Retry);
                _restart.onClick.AddListener(() => _pauseMenu.SetActive(false));
                _mainMenu.onClick.AddListener(_uiManager.GoToMainMenu);
                _mainMenu.onClick.AddListener(() => _pauseMenu.SetActive(false));
            }
            else
            {
                _restart.onClick.RemoveAllListeners();
                _mainMenu.onClick.RemoveAllListeners();
            }
            _levelIndicator.text = _uiManager.LevelName;
            gameObject.SetActive(isActive);
        }

    }
}
