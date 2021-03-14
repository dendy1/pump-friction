using UnityEngine;
using UnityEngine.Events;

namespace sdk_feed.Common.Events
{
    public class FeedDelayedEvent : AbstractEvent
    {
        [SerializeField] private UnityEvent _event;
        
        protected override void RunEvent () => _event?.Invoke();
    }
}