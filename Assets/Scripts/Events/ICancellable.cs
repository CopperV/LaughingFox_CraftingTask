namespace CopGameDev.RPG.Core.Events
{
    public interface ICancellable
    {
        bool Cancelled { get; set; }
    }
}
