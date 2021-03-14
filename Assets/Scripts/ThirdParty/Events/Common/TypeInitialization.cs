namespace sdk_feed.Common.Events
{
    public enum TypeInitialization
    {
        Custom, // Вызывается через метод Invoke
        
        OnPointerDown,
        OnPointerUp,
        
        OnStart,
        OnDestroy,
        
        OnEnable,
        OnDisable,
        
        OnMouseDown,
        OnMouseUp,
        
        OnAwake,
        
        OnTriggerEnter,
        OnTriggerExit,
        
        OnSwipeLeft,
        OnSwipeRight,
        OnSwipeUp,
        OnSwipeDown
    }
}