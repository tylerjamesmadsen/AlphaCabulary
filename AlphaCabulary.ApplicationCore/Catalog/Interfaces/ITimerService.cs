namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface ITimerService
    {
        void Start(int numSeconds);
        void Stop();
    }
}