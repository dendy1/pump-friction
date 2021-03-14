using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace sdk_feed.Common.Events.DelayedEvents
{
    [Serializable]
    public class Events
    {
        [SerializeField] private string _tag;
        [SerializeField] private TypeInitialization _typeAction = TypeInitialization.Custom;
        [SerializeField] private TypeRunEvents _typeRun = TypeRunEvents.AllDuringAction;
        [SerializeField] private List<Event> _events = new List<Event>();
        
        [SerializeField] private bool _isUseParamTag;
        [SerializeField] private string _paramTag;
        
        [SerializeField] private float _minSwipeLength = 100.0f;
        
        private bool _isSorted;

        
        public bool IsEqualType(in TypeInitialization type) => type == _typeAction;
        public bool IsEqualTag(in string tag) => _tag.Equals(tag);
        public float GetDuration() => _typeRun == TypeRunEvents.AllDuringAction ? _events.Last().delayInitialization : _events.Sum(evt => evt.delayInitialization);

        public void Run(MonoBehaviour owner, in Vector3 delta)
        {
            if (IsSwipeTypeCompare(delta))
            {
                Run(owner);
            }
        }

        private bool IsSwipeTypeCompare(Vector3 delta)
        {
            var dir = TypeInitialization.Custom; 
            if (Mathf.Abs(delta.x) > _minSwipeLength) dir = delta.x < 0 ? TypeInitialization.OnSwipeRight: TypeInitialization.OnSwipeLeft;
            if (Mathf.Abs(delta.y) > _minSwipeLength) dir = delta.y < 0? TypeInitialization.OnSwipeUp: TypeInitialization.OnSwipeDown;
            
            return _typeAction == dir;
        }
        public void Run(MonoBehaviour owner, in Func<string, bool> compareTag)
        {
            if (_isUseParamTag)
            {
                if (compareTag(_paramTag))
                {
                    Run(owner);
                }
            }
            else
            {
                Run(owner);
            }
        }
        
        public void Run(MonoBehaviour owner)
        {
            Sort();
            var calculateDelay = _typeRun == TypeRunEvents.AllDuringAction
                ? new Func<float, float, float>((delay, lastDelay) => delay - lastDelay)
                : (delay, lastDelay) => delay;
            
            owner.StartCoroutine(Run(calculateDelay));
        }

        private IEnumerator Run(Func<float, float, float> calculateDelay)
        {
            var lastDelay = 0f;
            foreach (var @event in _events)
            {
                var delay = calculateDelay(@event.delayInitialization, lastDelay);
                if (delay > 0.00001) yield return new WaitForSeconds(delay);
                
                @event.Invoke();
                lastDelay = @event.delayInitialization;
            }
        }

        private void Sort()
        {
            if (_typeRun != TypeRunEvents.AllDuringAction || _isSorted) return;

            _events.Sort((x, y) => x.delayInitialization.CompareTo(y.delayInitialization));
            _isSorted = true;
        }
    }
}
