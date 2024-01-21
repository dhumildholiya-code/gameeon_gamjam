using UnityEngine;

namespace GamJam.MyGrid
{
    [System.Serializable]
    public class GameGrid
    {
        public static readonly Vector2Int[] Direction = new Vector2Int[4]
        {
            Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.right
        };
        public const int North = 0;
        public const int West = 1;
        public const int South = 2;
        public const int East = 3;

        [SerializeField]
        private int _width;
        [SerializeField]
        private int _height;
        [SerializeField]
        private Vector2Int _origin;

        private float _halfCellSize;
        private Node[] _nodes;

        public int Width => _width;
        public int Height => _height;
        public Vector2Int Origin => _origin;

        public GameGrid(int width, int height, float cellSize, Vector2Int origin)
        {
            _width = width;
            _height = height;
            _origin = origin;
        }
        public void Init()
        {
            _halfCellSize = .5f;
            _nodes = new Node[_width * _height];
            for (int i = 0; i < _width * _height; i++)
            {
                _nodes[i] = new Node();
            }
        }
        public Node GetNode(Vector2 pos)
        {
            var gridId = PosToGridId(pos);
            int id = gridId.y * _width + gridId.x;
            if (id >= 0 && id < _nodes.Length)
                return _nodes[id];
            else return null;
        }
        public Node GetNode(Vector2Int gridId)
        {
            int id = gridId.y * _width + gridId.x;
            if (id >= 0 && id < _nodes.Length)
                return _nodes[id];
            else return null;
        }
        public bool IsValidIndex(Vector2Int id)
        {
            return id.x >= 0 && id.x < Width && id.y >= 0 && id.y < Height;
        }

        public Vector2 CellPosition(int x, int y)
        {
            return new Vector2(x, y) + _origin;
        }
        public Vector2 CellPosition(int index)
        {
            return new Vector2(index % _width, index / _width) + _origin;
        }

        public Vector2 CellCenterPosition(int x, int y)
        {
            return new Vector2(x, y) + _origin + Vector2.one * _halfCellSize;
        }
        public Vector2 CellCenterPosition(int index)
        {
            return new Vector2(index % _width, index / _width) + _origin + Vector2.one * _halfCellSize;
        }

        public void SetEntity(Entity e)
        {
            var gridId = e.GridPos;
            int id = gridId.y * _width + gridId.x;
            _nodes[id].AddEntity(e);
        }
        public void UnsetEntity(Entity e)
        {
            var gridId = e.GridPos;
            int id = gridId.y * _width + gridId.x;
            _nodes[id].RemoveEntity(e);
        }

        public Vector2Int PosToGridId(Vector2 position)
        {
            int x = Mathf.FloorToInt(position.x - _origin.x);
            int y = Mathf.FloorToInt(position.y - _origin.y);
            return new Vector2Int(x, y);
        }
    }
}
