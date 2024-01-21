using System.Collections.Generic;
using UnityEngine;

namespace GamJam.MyGrid
{
    public class GridVisual : MonoBehaviour
    {
        [SerializeField]
        private LineRenderer _linePrefab;

        private List<LineRenderer> _lines;

        public void DrawGrid(GameGrid grid)
        {
            _lines = new();
            //Draw Vertical Lines
            for (int i = 0; i <= grid.Width; i++)
            {
                Vector2 startPos = grid.CellPosition(i, 0);
                Vector2 endPos = new Vector2(0, grid.Height);
                LineRenderer line = Instantiate(_linePrefab, startPos, Quaternion.identity, transform);
                line.SetPosition(1, endPos);
                _lines.Add(line);
            }

            //Draw Horizontal Lines
            for (int i = 0; i <= grid.Height; i++)
            {
                Vector2 startPos = grid.CellPosition(0, i);
                Vector2 endPos = new Vector2(grid.Width, 0f);
                LineRenderer line = Instantiate(_linePrefab, startPos, Quaternion.identity, transform);
                line.SetPosition(1, endPos);
                _lines.Add(line);
            }
        }
        public void ClerGrid()
        {
        }
    }
}
