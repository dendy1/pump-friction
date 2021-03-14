using UnityEngine;
using UnityEngine.Events;


namespace sdk_feed.Common.Events
{
    public class FeedWhatIfEvent : MonoBehaviour
    {
        [SerializeField] private bool _value;

        [SerializeField] private UnityEvent _rightEvent;
        [SerializeField] private UnityEvent _wrongEvent;


        public void Toggle(bool value)
        {
            Set(value);
            Check();
        }
        public void Set(bool value) => _value = value;


        public void Check()
        {
            if (_value)
            {
                _rightEvent?.Invoke();
            }
            else
            {
                _wrongEvent?.Invoke();
            }
        }
    }
}
