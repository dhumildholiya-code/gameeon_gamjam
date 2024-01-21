using UnityEngine;

namespace GamJam
{
    public class LevelCompleteUi : MonoBehaviour
    {
        [SerializeField]
        private MenuButton _nextLevel;
        [SerializeField]
        private MenuButton _retry;
        [SerializeField]
        private MenuButton _mainMenu;

        private UiManager _uiManager;

        public void Init(UiManager uiManager)
        {
            _uiManager = uiManager;
        }
        public void Exit()
        {
            _uiManager = null;
        }

        public void Show(bool isActive)
        {
            if (isActive)
            {
                _nextLevel.onClick.AddListener(() => _uiManager.NextLevel());
                _retry.onClick.AddListener(_uiManager.Retry);
                _mainMenu.onClick.AddListener(_uiManager.GoToMainMenu);
            }
            else
            {
                _nextLevel.onClick.RemoveAllListeners();
                _retry.onClick.RemoveAllListeners();
                _mainMenu.onClick.RemoveAllListeners();
            }
            gameObject.SetActive(isActive);
        }

    }
}
