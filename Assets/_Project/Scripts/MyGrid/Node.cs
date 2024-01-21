using System.Collections.Generic;
using UnityEngine;

namespace GamJam.MyGrid
{
    public class Node
    {
        private List<Entity> _entities;
        public List<Entity> Entities => _entities;
        public int EntityCount => _entities.Count;

        public Node()
        {
            _entities = new();
        }

        public void AddEntity(Entity e)
        {
            _entities.Add(e);
        }
        public void RemoveEntity(Entity e)
        {
            _entities.Remove(e);
        }

        public T Get<T>()
        {
            foreach (Entity e in _entities)
            {
                if (e.TryGetComponent(out T comp))
                {
                    return comp;
                }
            }
            return default;
        }
    }
}
