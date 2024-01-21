using GamJam.EventChannel;
using GamJam.GameStates;
using UnityEngine;

namespace GamJam.Gameplay
{
    public class Ninja : GameBehaviour
    {
        [SerializeField]
        private GameStateEventChannel _onStateChange;
        [Header("SFX")]
        [SerializeField]
        private AudioData _moveSoundData;
        [SerializeField]
        private AudioData _dieSound;

        public bool HasKey { get; set; }

        public void Die()
        {
            _dieSound.Play();
            _onStateChange.Invoke(GameState.LevelFailed);
        }
    }
}
