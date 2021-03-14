using System;
using UnityEngine;
using UnityEngine.EventSystems;
using sdk_feed.Common.Extensions;

namespace sdk_feed.Common.Events
{
    public abstract class AbstractEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private TypeInitialization _typeInitialization = TypeInitialization.Custom;
        [SerializeField] private float _delayInitialization = 2.0f;
        
        [SerializeField] private float _minSwipeLengthByX = 50.0f;
        [SerializeField] private float _minSwipeLengthByY = 100.0f;
        
        [SerializeField] private bool _isUseTag;
        [SerializeField] private string _tag;

        private Vector3 _touchPosition;

        public float DelayInitialization
        {
            set => _delayInitialization = value;
        }

        private void Awake()
        {
            if (_typeInitialization == TypeInitialization.OnAwake) Run();
        }

        private void Start()
        {
            if (_typeInitialization == TypeInitialization.OnStart) Run();
        }
        
        
        private void OnDestroy()
        {
            if (_typeInitialization == TypeInitialization.OnDestroy) Run();
        }
        
        
        private void OnEnable()
        {
            if (_typeInitialization == TypeInitialization.OnEnable) Run();
        }
        
        
        private void OnDisable()
        {
            if (_typeInitialization == TypeInitialization.OnDisable) Run();
        }

        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (_typeInitialization == TypeInitialization.OnPointerDown) Run();
            if (IsSwipeType()) _touchPosition = Input.mousePosition;
        }

        
        public void Invoke()
        {
            if (_typeInitialization == TypeInitialization.Custom) Run();
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            if (_typeInitialization == TypeInitialization.OnPointerUp) Run();
            if (IsSwipeType() && IsSwipeTypeCompare(_typeInitialization)) Run();
        }


        private void OnMouseDown()
        {
            if (_typeInitialization == TypeInitialization.OnMouseDown) Run();
            if (IsSwipeType()) _touchPosition = Input.mousePosition;
        }

        private void OnMouseUp()
        {
            if (_typeInitialization == TypeInitialization.OnMouseUp) Run();
            if (IsSwipeType() && IsSwipeTypeCompare(_typeInitialization)) Run();
        }

        private bool IsSwipeType()
        {
            // ReSharper disable once PossibleNullReferenceException
            return Enum.GetName(typeof(TypeInitialization), _typeInitialization).IndexOf("OnSwipe", StringComparison.Ordinal) != -1;
        }

        private bool IsSwipeTypeCompare(in TypeInitialization type)
        {
            Vector2 delta = _touchPosition - Input.mousePosition;

            var dir = TypeInitialization.Custom; 
            if (Mathf.Abs(delta.x) > _minSwipeLengthByX) dir = delta.x < 0 ? TypeInitialization.OnSwipeRight: TypeInitialization.OnSwipeLeft;
            if (Mathf.Abs(delta.y) > _minSwipeLengthByY) dir = delta.y < 0? TypeInitialization.OnSwipeUp: TypeInitialization.OnSwipeDown;
            
            return type == dir;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_typeInitialization != TypeInitialization.OnTriggerEnter) return;
            RunTriggerEvent(other.CompareTag);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_typeInitialization != TypeInitialization.OnTriggerExit) return;
            RunTriggerEvent(other.CompareTag);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_typeInitialization != TypeInitialization.OnTriggerEnter) return;
            RunTriggerEvent(other.CompareTag);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_typeInitialization != TypeInitialization.OnTriggerExit) return;
            RunTriggerEvent(other.CompareTag);
        }

        private void RunTriggerEvent(in Func<string, bool> compareTag)
        {
            if (_isUseTag)
            {
                if (compareTag(_tag))
                {
                    Run();
                }
            }
            else
            {
                Run();
            }
        }

        private void Run()
        {
            if (Math.Abs(_delayInitialization) < 0.0001)
            {
                RunEvent();
            }
            else
            {
                this.Invoke(RunEvent, _delayInitialization);
            }
        }
        protected abstract void RunEvent();
    }
}