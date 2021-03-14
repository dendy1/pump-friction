using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace sdk_feed.Common.Events
{
    public class FeedLoopedEvent : AbstractEvent
    {
        [SerializeField] private float _delayBetweenEvents = 0.5f;
        [SerializeField] private UnityEvent _event;

        private Coroutine _coroutine;
        
        protected override void RunEvent()
        {
            Stop();
            _coroutine = StartCoroutine(InvokeCoroutine());
        }
        
        public void Stop()
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
        }

        private IEnumerator InvokeCoroutine()
        {
            while (true)
            {
                _event?.Invoke();
                if (_delayBetweenEvents > 0.00001) yield return new WaitForSeconds(_delayBetweenEvents);
            }
            // ReSharper disable once IteratorNeverReturns
        }
    }
}