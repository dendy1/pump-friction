using System;
using UnityEngine;
using UnityEngine.Events;

namespace sdk_feed.Common.Events.DelayedEvents
{
    [Serializable]
    internal class Event
    {
        [SerializeField] private float _delayInitialization = 2.0f;
        [SerializeField] private UnityEvent _event;

        public float delayInitialization => _delayInitialization;

        public void Invoke() => _event.Invoke();
    }
}