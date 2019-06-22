using System;
using System.Threading.Tasks;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface ITimer
    {
        event EventHandler OnTick;

        Task Start(int numSeconds);
        void Stop();
    }
}