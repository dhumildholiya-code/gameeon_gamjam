using UnityEngine;

namespace GamJam.Gameplay
{
    public sealed class SpikeTrap : GameBehaviour, ISwitchable
    {
        [SerializeField]
        private SpriteRenderer _render;
        [SerializeField]
        private Sprite[] _visuals;

        private bool _isSpike;

        public override void Init(Entity entity)
        {
            base.Init(entity);
        }
        public void Switch(bool isOn)
        {
            _isSpike = isOn;
            _render.sprite = _isSpike ? _visuals[0] : _visuals[1];
        }
        public override void OnTrigger(Entity e)
        {
            if (e.TryGetComponent(out Ninja ninja))
            {
                if (_isSpike) ninja.Die();
            }
        }
    }
}
