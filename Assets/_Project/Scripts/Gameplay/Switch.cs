using UnityEngine;

namespace GamJam.Gameplay
{
    [RequireComponent(typeof(Collidable))]
    public sealed class Switch : GameBehaviour
    {
        [System.Serializable]
        public class SwitchData
        {
            public bool reversed;
            public GameObject go;
        }

        [SerializeField]
        private bool _isOn;
        [SerializeField]
        private SwitchData[] _switchData;
        [Header("Visual")]
        [SerializeField]
        private SpriteRenderer _render;
        [SerializeField]
        private Sprite[] _sprites;
        [SerializeField]
        private LineRenderer[] _lines;

        private ISwitchable[] _switchable;

        public override void Init(Entity entity)
        {
            base.Init(entity);
            _switchable = new ISwitchable[_switchData.Length];
            for (int i = 0; i < _switchable.Length; i++)
            {
                _switchable[i] = _switchData[i].go.GetComponent<ISwitchable>();
            }
            SwitchOn(false);

        }
        public void DrawConnection()
        {
            for (int i = 0; i < _switchable.Length; i++)
            {
                var target = _entity.Grid.PosToGridId(_switchData[i].go.transform.position);
                int x = target.x - Position.x;
                int y = target.y - Position.y;
                _lines[i].gameObject.SetActive(true);
                _lines[i].SetPosition(0, _entity.Grid.CellCenterPosition(Position.x, Position.y));
                _lines[i].SetPosition(1, _entity.Grid.CellCenterPosition(Position.x + x, Position.y));
                _lines[i].SetPosition(2, _entity.Grid.CellCenterPosition(target.x, Position.y + y));
            }
        }
        public override void OnTrigger(Entity e)
        {
            SwitchOn();
        }
        public override void TriggetExit()
        {
            SwitchOn();
        }

        private void SwitchOn(bool changeState = true)
        {
            if (changeState)
                _isOn = !_isOn;
            _render.sprite = _isOn ? _sprites[0] : _sprites[1];
            for (int i = 0; i < _switchable.Length; i++)
            {
                _switchable[i].Switch(_switchData[i].reversed ? !_isOn : _isOn);
            }
        }
    }
}
