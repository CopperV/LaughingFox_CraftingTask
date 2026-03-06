namespace CopGameDev.RPG.Core.Events
{
    public interface IEvent { }

    public static class IEventExtensions
    {
        public static void Invoke<T>(this T @event) where T : IEvent
        {
            EventBus<T>.Raise(@event);
        }
    }
}
