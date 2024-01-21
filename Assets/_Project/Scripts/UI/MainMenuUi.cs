using UnityEngine;

namespace GamJam
{
    public class MainMenuUi : MonoBehaviour
    {
        [SerializeField] private MenuButton options;
        [SerializeField] private MenuButton quit;

        private UiManager _uiManager;

        public void Init(UiManager uiManager)
        {
            _uiManager = uiManager;
            options.onClick.AddListener(_uiManager.OptionsButton);
            quit.onClick.AddListener(_uiManager.QuitButton);
        }
        public void Exit()
        {
            _uiManager = null;
            options.onClick.RemoveAllListeners();
            quit.onClick.RemoveAllListeners();
        }

        public void Show(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
