using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamJam.EventChannel
{

    [CreateAssetMenu(fileName = "GameEvent Channel", menuName = "Events/Game Event")]
    public class GameEventChannel : ScriptableObject
    {
        private List<Action> _listeners = new();

        public void Register(Action action)
        {
            _listeners.Add(action);
        }
        public void Unregister(Action action)
        {
            _listeners.Remove(action);
        }
        public void Invoke()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].Invoke();
            }
        }
    }
}
