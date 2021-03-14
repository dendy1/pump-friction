using System;
using UnityEngine.Events;

namespace sdk_feed.Common.Events
{
    [Serializable] public class UnityEventInt : UnityEvent<int>  {}
    [Serializable] public class UnityEventFloat : UnityEvent<float>  {}
    [Serializable] public class UnityEventBool : UnityEvent<bool>  {}
}