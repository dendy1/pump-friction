using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace sdk_feed.Common.Events.DelayedEvents
{
    public class FeedDelayedEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private List<Events> _events;

        private Vector3 _startTouchPosition;
        

        private void Awake() => Run(TypeInitialization.OnAwake);
        private void Start() => Run(TypeInitialization.OnStart);
        private void OnDestroy() => Run(TypeInitialization.OnDestroy);
        private void OnEnable() => Run(TypeInitialization.OnEnable);
        private void OnDisable() => Run(TypeInitialization.OnDisable);
        
        public void OnPointerDown(PointerEventData eventData)
        {
            Run(TypeInitialization.OnPointerDown);
            _startTouchPosition = Input.mousePosition;
        }
        
        public void OnPointerUp(PointerEventData eventData)
        {
            Run(TypeInitialization.OnPointerUp);
            Run(TypeInitialization.OnSwipeDown, _startTouchPosition - Input.mousePosition);
            Run(TypeInitialization.OnSwipeLeft, _startTouchPosition - Input.mousePosition);
            Run(TypeInitialization.OnSwipeRight, _startTouchPosition - Input.mousePosition);
            Run(TypeInitialization.OnSwipeUp, _startTouchPosition - Input.mousePosition);
        }
        
        public void OnMouseDown()
        {
            Run(TypeInitialization.OnMouseDown);
            _startTouchPosition = Input.mousePosition;
        }
        
        public void OnMouseUp()
        {
            Run(TypeInitialization.OnMouseUp);
            Run(TypeInitialization.OnSwipeDown, _startTouchPosition - Input.mousePosition);
            Run(TypeInitialization.OnSwipeLeft, _startTouchPosition - Input.mousePosition);
            Run(TypeInitialization.OnSwipeRight, _startTouchPosition - Input.mousePosition);
            Run(TypeInitialization.OnSwipeUp, _startTouchPosition - Input.mousePosition);
        }
        
        public void OnTriggerEnter(Collider other) => Run(TypeInitialization.OnTriggerEnter, other.CompareTag);
        public void OnTriggerExit(Collider other) => Run(TypeInitialization.OnTriggerExit, other.CompareTag);
        public void OnTriggerEnter2D(Collider2D other) => Run(TypeInitialization.OnTriggerEnter, other.CompareTag);
        public void OnTriggerExit2D(Collider2D other) => Run(TypeInitialization.OnTriggerExit, other.CompareTag);

        public void Invoke()
        {
            var eventsSelectedByType = _events.Where(evt => evt.IsEqualType(TypeInitialization.Custom) && evt.IsEqualTag("")).ToArray();
            if (!eventsSelectedByType.Any()) return;
            
            foreach (var evt in eventsSelectedByType) evt.Run(this);
        }

        public void Invoke(string customTag)
        {
            var eventsSelectedByType = _events.Where(evt => evt.IsEqualType(TypeInitialization.Custom) && evt.IsEqualTag(customTag)).ToArray();
            if (!eventsSelectedByType.Any()) return;
            
            foreach (var evt in eventsSelectedByType) evt.Run(this);
        }

        private void Run(TypeInitialization type, Vector3 delta)
        {
            var eventsSelectedByType = _events.Where(evt => evt.IsEqualType(type)).ToArray();
            if (!eventsSelectedByType.Any()) return;
            
            var obj = GetTempObject(type);
            foreach (var evt in eventsSelectedByType) evt.Run(obj, delta);
        }
        
        private void Run(TypeInitialization type, in Func<string, bool> compareTag)
        {
            var eventsSelectedByType = _events.Where(evt => evt.IsEqualType(type)).ToArray();
            if (!eventsSelectedByType.Any()) return;
            
            var obj = GetTempObject(type);
            foreach (var evt in eventsSelectedByType) evt.Run(obj, compareTag);
        }
        
        private void Run(TypeInitialization type)
        {
            var eventsSelectedByType = _events.Where(evt => evt.IsEqualType(type)).ToArray();
            if (!eventsSelectedByType.Any()) return;
            
            var obj = GetTempObject(type);
            foreach (var evt in eventsSelectedByType) evt.Run(obj);
        }

        private MonoBehaviour GetTempObject(TypeInitialization type)
        {
            if (!(type == TypeInitialization.OnDestroy || type == TypeInitialization.OnDisable)) return this;
            
            var obj = new GameObject(name + "_delay_destroyed");
            var component = obj.AddComponent<DelayDestroyedObject>();
            var duration = _events.Where(evt => evt.IsEqualType(type)).Max(evt => evt.GetDuration()) + 1;
            component.Destroy(duration);
            return component;
        }
    }
}