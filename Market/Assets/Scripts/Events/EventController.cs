using InventorySystem.Audio;
using System;


namespace InventorySystem.Events
{
    public class GameEventController
    {
        public event Action baseEvent;
        public void InvokeEvent() => baseEvent?.Invoke();
        public void AddListener(Action listener) => baseEvent += listener;
        public void RemoveListener(Action listener) => baseEvent -= listener;
    }

    public class GameEventController<T>
    {
        public event Action<T> baseEvent;
        public void InvokeEvent(T type) => baseEvent?.Invoke(type);
        public void AddListener(Action<T> listener) => baseEvent += listener;
        public void RemoveListener(Action<T> listener) => baseEvent -= listener;
    }

    public class GameEventController<T1, T2>
    {
        public event Action<T1, T2> baseEvent;
        public void InvokeEvent(T1 type1, T2 type2) => baseEvent?.Invoke(type1, type2);
        public void AddListener(Action<T1, T2> listener) => baseEvent += listener;
        public void RemoveListener(Action<T1, T2> listener) => baseEvent -= listener;
    }
}