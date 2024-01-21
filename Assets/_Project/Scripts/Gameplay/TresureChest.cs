using GamJam.EventChannel;
using GamJam.GameStates;
using UnityEngine;

namespace GamJam.Gameplay
{
    public class TresureChest : GameBehaviour
    {
        [SerializeField]
        private SpriteRenderer _render;
        [SerializeField]
        private Sprite _open;
        [Header("Game Event To Invoke")]
        [SerializeField]
        private GameStateEventChannel _onLevelComplete;
        [Header("SFX")]
        [SerializeField]
        private AudioData _openSound;
        [SerializeField]
        private AudioData _denySound;

        private void Open(Ninja ninja)
        {
            if (ninja.HasKey)
            {
                _render.sprite = _open;
                _openSound.Play();
                _onLevelComplete.Invoke(GameState.LevelComplete);
            }
            else
            {
                _denySound.Play();
            }
        }

        public override void OnCollide(Entity e)
        {
            if (e.TryGetComponent(out Ninja ninja))
            {
                Open(ninja);
            }
        }
    }
}
