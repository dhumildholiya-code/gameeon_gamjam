using GamJam.MyGrid;
using UnityEngine;

namespace GamJam.Gameplay
{
    public class Laser : GameBehaviour, ISwitchable
    {
        public int direction;
        public int length;
        [Header("Visual")]
        [SerializeField]
        private LineRenderer _line;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _end;

        private Vector2Int _step;
        private bool _isHorizontal;


        public override void Init(Entity entity)
        {
            base.Init(entity);
            Raycast();
        }

        public void Raycast()
        {
            Vector2 startPos = _entity.Grid.CellCenterPosition(Position.x, Position.y);
            _line.SetPosition(0, startPos);
            _start.up = (Vector2)GameGrid.Direction[direction];
            _start.position = startPos - (Vector2)_start.up * .5f;
            for (int i = 0; i < length; i++)
            {
                _step = Position + i * GameGrid.Direction[direction];
                if (_entity.Grid.IsValidIndex(_step))
                {
                    Node node = _entity.Grid.GetNode(_step);
                    if (node.EntityCount == 0) continue;
                    Blockable block = node.Get<Blockable>();
                    Ninja ninja = node.Get<Ninja>();
                    if (block != null)
                    {
                        if (block.destrcutible)
                        {
                            block.Entity.Kill();
                        }
                        else
                        {
                            _end.gameObject.SetActive(false);
                            break;
                        }
                    }
                    if (ninja != null)
                    {
                        _end.gameObject.SetActive(false);
                        ninja.Die();
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            Vector2 endPos = _entity.Grid.CellCenterPosition(_step.x, _step.y);
            _end.position = endPos + (Vector2)_start.up * .5f;
            _end.up = -_start.up;
            _line.SetPosition(1, endPos);
        }

        public void Switch(bool isOn)
        {
            _isHorizontal = isOn;
            int rotateDir = direction - 1;
            if (rotateDir < 0) rotateDir += 4;
            direction = _isHorizontal ? direction : rotateDir;
        }
    }
}
