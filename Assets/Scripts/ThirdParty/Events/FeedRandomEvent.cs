using sdk_feed.Common.Extensions;
using UnityEngine;
using UnityEngine.Events;


namespace sdk_feed.Common.Events
{
    /// <summary>
    /// Класс хрант массив событий, по вызову метода invoke случайно выбирает из массива одно событие и вызывает его
    /// </summary>
    public class FeedRandomEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent[] _events;
        public void Invoke() => _events.GetRandomElement()?.Invoke();
    }
}