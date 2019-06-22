using System.Threading.Tasks;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IGameService
    {
        ITimer Timer { get; }
        Task StartAsync(int numSeconds);
        void Stop(bool isCancelled);
        bool IsRunning { get; set; }
    }
}