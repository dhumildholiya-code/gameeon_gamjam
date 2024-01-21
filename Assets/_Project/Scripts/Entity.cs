using GamJam.MyGrid;
using System.Collections.Generic;
using UnityEngine;

namespace GamJam
{
    public sealed class Entity : MonoBehaviour
    {
        private int _id;
        private bool _isAlive;
        [SerializeField]
        private Vector2Int _startIndex;

        public int Id => _id;
        public bool IsAlive => _isAlive;
        public Vector2Int GridPos { get; set; }

        private GameGrid _grid;
        public GameGrid Grid => _grid;

        private List<GameBehaviour> _behaviours;

        public void Init(int id, GameGrid grid)
        {
            _isAlive = true;
            _id = id; _grid = grid;
            _startIndex = new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y));
            GridPos = _startIndex;
            transform.position = _grid.CellCenterPosition(GridPos.x, GridPos.y);

            _grid.SetEntity(this);
            _behaviours = new List<GameBehaviour>();
            GetComponents<GameBehaviour>(_behaviours);
            foreach (var behaviour in _behaviours)
            {
                behaviour.Init(this);
            }
        }
        public void OnTriggerCall(Entity e)
        {
            for (int i = _behaviours.Count - 1; i >= 0; i--)
            {
                _behaviours[i].OnTrigger(e);
            }
        }
        public void TriggerExitCall()
        {
            for (int i = _behaviours.Count - 1; i >= 0; i--)
            {
                _behaviours[i].TriggetExit();
            }
        }
        public void OnCollisionCall(Entity e)
        {
            for (int i = _behaviours.Count - 1; i >= 0; i--)
            {
                _behaviours[i].OnCollide(e);
            }
        }
        public void Kill()
        {
            _isAlive = false;
            gameObject.SetActive(false);
        }
        public void Exit()
        {
            foreach (var behaviour in _behaviours)
            {
                behaviour.Exit();
            }
            _behaviours.Clear();
        }
    }
}
