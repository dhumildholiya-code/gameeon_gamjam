using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamJam.EventChannel
{
    public abstract class BaseEventChannel<T> : ScriptableObject
    {
        protected List<Action<T>> _listeners = new();
        public void Register(Action<T> action)
        {
            _listeners.Add(action);
        }
        public void Unregister(Action<T> action)
        {
            _listeners.Remove(action);
        }
        public void Invoke(T data)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].Invoke(data);
            }
        }
    }
}
