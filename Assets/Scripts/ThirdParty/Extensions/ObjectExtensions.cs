using System;
using System.Collections;
using UnityEngine;

namespace sdk_feed.Common.Extensions
{
    public static class  ObjectExtensions
    {
    public static IEnumerator InvokeAfterDelay(this object obj, Action action, float delay)
    {
        return InvokeAfterDelay(action, delay);
    }
        internal static IEnumerator InvokeAfterDelay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action?.Invoke();
        }
        
    }
}