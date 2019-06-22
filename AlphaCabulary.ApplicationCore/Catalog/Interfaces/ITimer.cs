using System;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface ITimer
    {
        event EventHandler<TimerEventArgs> TimerTickEventHandler;

        Task StartAsync(int numSeconds);
        void Stop();
    }
}