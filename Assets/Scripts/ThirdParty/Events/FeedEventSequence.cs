using UnityEngine;
using System.Collections.Generic;
using sdk_feed.Common.Extensions;
using UnityEngine.Events;

namespace sdk_feed.Common.Events
{

    [System.Serializable]
    public class SequenceUnitEvent
    {
        public string name = "событие";
        public float timeToNext;
        public UnityEvent _event;

    }
    internal class FeedEventSequence : AbstractEvent
    {
        [SerializeField] private List<SequenceUnitEvent> sequence;
        protected override void RunEvent ()
        {
            StopAllCoroutines();
            StartSequence();
        }
        
        private void StartSequence()
        {
            float currentDelay = 0;
            foreach (var seqUnit in sequence)
            {
                currentDelay += seqUnit.timeToNext;
                this.Invoke(seqUnit._event.Invoke, currentDelay);

            }
        }
    }
}