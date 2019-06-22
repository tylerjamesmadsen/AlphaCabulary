namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IGameService
    {
        void Start();
        void Stop(bool isCancelled);
        bool IsRunning { get; set; }
    }
}