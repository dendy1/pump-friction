namespace sdk_feed.Common.Events
{
    public class FeedDelayedDestroyObject : AbstractEvent
    {
        protected override void RunEvent() => Destroy(gameObject);
    }
}