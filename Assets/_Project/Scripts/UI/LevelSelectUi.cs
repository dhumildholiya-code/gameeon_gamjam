using System;
using UnityEngine;

namespace GamJam
{
    public class LevelSelectUi : MonoBehaviour
    {
        [SerializeField]
        private LevelItem _itemPrefab;
        [SerializeField]
        private Transform _container;

        private LevelItem[] _items;
        private UiManager _uiManager;

        public void Init(UiManager uiManager, LevelData[] data)
        {
            _uiManager = uiManager;
            _items = new LevelItem[data.Length];
            GameManager.OnLevelUnlocked += HandleLevelUnlocked;
            for (int i = 0; i < data.Length; i++)
            {
                _items[i] = Instantiate(_itemPrefab, _container);
                int levelId = i;
                _items[i].Init(levelId, data[i], () =>
                {
                    _uiManager.NextLevel(levelId);
                    gameObject.SetActive(false);
                });
            }
        }
        public void Exit()
        {
            GameManager.OnLevelUnlocked += HandleLevelUnlocked;
            for (int i = _items.Length - 1; i >= 0; i--)
            {
                Destroy(_items[i]);
            }
            _items = null;
            _uiManager = null;
        }

        private void HandleLevelUnlocked(int id)
        {
            _items[id].Unlock();
        }

        public void Show(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

    }
}
