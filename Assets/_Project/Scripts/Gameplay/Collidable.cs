using System.Collections.Generic;

namespace GamJam.Gameplay
{
    public class Collidable : GameBehaviour
    {
        public bool isTrigger;

        private HashSet<Collidable> _colliders = new();

        public int Count => _colliders.Count;
        public bool HasInteracted(Collidable collidable)
        {
            return _colliders.Contains(collidable);
        }
        public void Add(Collidable collidable)
        {
            _colliders.Add(collidable);
        }
        public void Remove(Collidable collidable)
        {
            _colliders.Remove(collidable);
        }
        public void Clear() => _colliders.Clear();
    }
}
