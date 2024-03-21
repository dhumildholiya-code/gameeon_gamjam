using GamJam.Gameplay;
using GamJam.MyGrid;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SearchService;

namespace GamJam
{
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private GameGrid _grid;
        [SerializeField]
        private GridVisual _gridVisual;

        private List<Entity> _entities;
        private PlayerInput _input;
        private List<Movable> _movables = new();
        private List<Collidable> _collidables = new();
        private List<Laser> _lasers = new();
        private List<Switch> _switches = new();
        private Movable _ninjaMovable;

        private bool _isRunning;

        public void Init()
        {
            _grid.Init();
            _gridVisual.DrawGrid(_grid);

            //gameplay Initialization
            _movables = new();
            _entities = FindObjectsByType<Entity>(FindObjectsSortMode.None).ToList();
            for (int i = 0; i < _entities.Count; i++)
            {
                _entities[i].Init(i, _grid);
                if (_entities[i].TryGetComponent(out Collidable collider))
                {
                    _collidables.Add(collider);
                }
                if (_entities[i].TryGetComponent(out Movable movable))
                {
                    if (_entities[i].TryGetComponent(out PlayerInput input))
                    {
                        _input = input;
                        _ninjaMovable = movable;
                    }
                    _movables.Add(movable);
                }
                if (_entities[i].TryGetComponent(out Laser laser))
                {
                    _lasers.Add(laser);
                }
                if (_entities[i].TryGetComponent(out Switch swi))
                {
                    _switches.Add(swi);
                }
            }
            for (int i = 0; i < _switches.Count; i++)
            {
                _switches[i].DrawConnection();
            }
        }


        public IEnumerator Exit()
        {
            while (_isRunning)
            {
                yield return null;
            }
            _input = null;
            _ninjaMovable = null;
            _collidables.Clear();
            _movables.Clear();
            _entities.Clear();
        }
        private async Task HandleTurn()
        {
            //Will Do Player action.
            _isRunning = true;
            if (_ninjaMovable.Move(_input.direction))
            {
                //then all the other actions.
                CollisionSystem();
                RaycastSystem();
                RemoveDeadEntites();
                await Task.Delay(100);
            }
            _isRunning = false;
        }
        private async void Update()
        {
            await InputSystemAsync();
        }
        private void RemoveDeadEntites()
        {
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                if (_entities[i].IsAlive) continue;
                _entities.RemoveAt(i);
            }
            for (int i = _collidables.Count - 1; i >= 0; i--)
            {
                if (_collidables[i].IsAlive) continue;
                _collidables.RemoveAt(i);
            }
        }
        private async Task InputSystemAsync()
        {
            if (_input is null) return;
            if (Input.GetKeyDown(KeyCode.W))
            {
                _input.direction = GameGrid.North;
                await HandleTurn();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                _input.direction = GameGrid.South;
                await HandleTurn();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _input.direction = GameGrid.West;
                await HandleTurn();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _input.direction = GameGrid.East;
                await HandleTurn();
            }
        }

        private void CollisionSystem()
        {
            for (int i = _collidables.Count - 1; i >= 0; i--)
            {
                for (int j = _collidables.Count - 1; j >= 0; j--)
                {
                    if (i == j) continue;
                    Collidable other = _collidables[j];
                    if (_collidables[i].Position != other.Position) continue;
                    if (!_collidables[i].isTrigger && !other.isTrigger)
                    {
                        //Collision Resolutions depending on type of properties.
                        CollisionResolution(_collidables[i], other);

                        _collidables[i].Entity.OnCollisionCall(other.Entity);
                        other.Entity.OnCollisionCall(_collidables[i].Entity);
                    }
                }
            }
            for (int i = _collidables.Count - 1; i >= 0; i--)
            {
                for (int j = _collidables.Count - 1; j >= 0; j--)
                {
                    if (i == j) continue;
                    Collidable other = _collidables[j];
                    if (_collidables[i] == other) continue;
                    if (_collidables[i].Position != other.Position)
                    {
                        if (_collidables[i].HasInteracted(other) && other.HasInteracted(_collidables[i]))
                        {
                            _collidables[i].Entity.TriggerExitCall();
                            other.Entity.TriggerExitCall();
                            _collidables[i].Remove(other);
                            other.Remove(_collidables[i]);
                        }
                        continue;
                    }
                    if (!_collidables[i].isTrigger && !_collidables[i].HasInteracted(other)
                        && other.isTrigger && !other.HasInteracted(_collidables[i]))
                    {
                        _collidables[i].Add(other);
                        other.Add(_collidables[i]);
                        _collidables[i].Entity.OnTriggerCall(other.Entity);
                        other.Entity.OnTriggerCall(_collidables[i].Entity);
                    }
                }
            }
        }

        private void RaycastSystem()
        {
            for (int i = _lasers.Count - 1; i >= 0; i--)
            {
                _lasers[i].Raycast();
            }
        }

        private void CollisionResolution(Collidable first, Collidable second)
        {
            Movable movable;
            Pushable pushable;
            if ((first.TryGetComponent(out movable) && second.TryGetComponent(out pushable))
                || (second.TryGetComponent(out movable) && first.TryGetComponent(out pushable)))
            {
                //If Movable can not force other objects.
                if (!movable.hasForce)
                {
                    movable.Position = movable.PrevPosition;
                    return;
                }
                Vector2Int dir = movable.Position - movable.PrevPosition;
                Vector2Int targetPos = dir + pushable.Position;
                Collidable targetCellCollider = _grid.GetNode(targetPos)?.Get<Collidable>();
                bool canMove = targetCellCollider == null || (targetCellCollider != null && targetCellCollider.isTrigger);
                if (_grid.IsValidIndex(pushable.Position + dir) && canMove)
                {
                    pushable.Position = targetPos;
                }
                else
                {
                    movable.Position = movable.PrevPosition;
                }
            }
            else if (first.TryGetComponent(out movable) || second.TryGetComponent(out movable))
            {
                movable.Position = movable.PrevPosition;
            }
        }
    }
}
