using sdk_feed.Common.Extensions;
using UnityEngine;

namespace sdk_feed.Common.Events.DelayedEvents
{
    /// <inheritdoc />
    /// <summary>Класс служит для уничтожения временного объекта и запуска корутин на нем</summary>
    internal class DelayDestroyedObject : MonoBehaviour
    {
        internal void Destroy(float delay) => this.Invoke(delegate { Destroy(gameObject); }, delay);
    }
}