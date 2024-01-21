using GamJam.EventChannel;
using UnityEngine;

namespace GamJam.Gameplay
{
    public class Key : GameBehaviour
    {
        [Header("SFX")]
        [SerializeField]
        private AudioData _collectSound;

        public override void OnTrigger(Entity e)
        {
            if (e.TryGetComponent(out Ninja ninja))
            {
                ninja.HasKey = true;
                _collectSound.Play();
                UnsetEntity();
                Entity.Kill();
            }
        }
    }
}
