using System;
using UnityEngine;

namespace sdk_feed.Common.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void Invoke(this MonoBehaviour context, Action action, float delay)
        {
            context.StartCoroutine(ObjectExtensions.InvokeAfterDelay(action, delay));
        }
    }
}