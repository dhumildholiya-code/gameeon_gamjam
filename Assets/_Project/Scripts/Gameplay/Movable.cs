using GamJam.MyGrid;
using UnityEngine;

namespace GamJam.Gameplay
{
    public class Movable : GameBehaviour
    {
        public int step = 1;
        public bool inverseDirOnNotValid;
        public bool hasForce;
        [Header("Visual")]
        [SerializeField]
        private SpriteRenderer _render;
        [SerializeField]
        private Sprite[] _sprites;

        public Vector2Int PrevPosition { get; private set; }

        public override void Init(Entity entity)
        {
            base.Init(entity);
            _render.sprite = _sprites[GameGrid.South];
        }

        public bool Move(int direction)
        {
            UpdateVisual(direction);
            PrevPosition = Position;
            if (_entity.Grid.IsValidIndex(Position + GameGrid.Direction[direction]))
            {
                Position += GameGrid.Direction[direction];
                transform.position = _entity.Grid.CellCenterPosition(Position.x, Position.y);
                return true;
            }
            if (inverseDirOnNotValid)
                UpdateVisual((direction + 2) % 4);
            return false;
        }

        public void UpdateVisual(int direction)
        {
            _render.sprite = _sprites[direction];
        }
    }
}
