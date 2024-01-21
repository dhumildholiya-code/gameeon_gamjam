using UnityEngine;

namespace GamJam
{
    [RequireComponent(typeof(Entity))]
    public abstract class GameBehaviour : MonoBehaviour
    {
        protected Entity _entity;
        public Entity Entity => _entity;
        public bool IsAlive => _entity.IsAlive;
        public Vector2Int Position
        {
            get => _entity.GridPos;
            set
            {
                UnsetEntity();
                transform.position = _entity.Grid.CellCenterPosition(value.x, value.y);
                _entity.GridPos = value;
                SetEntity();
            }
        }
        public virtual void Init(Entity entity)
        {
            _entity = entity;
        }
        public virtual void Exit()
        {
        }
        protected void SetEntity()
        {
            _entity.Grid.SetEntity(_entity);
        }
        protected void UnsetEntity()
        {
            _entity.Grid.UnsetEntity(_entity);
        }

        public virtual void OnTrigger(Entity e)
        {
        }
        public virtual void TriggetExit()
        {
        }
        public virtual void OnCollide(Entity e)
        {
        }
    }
}
