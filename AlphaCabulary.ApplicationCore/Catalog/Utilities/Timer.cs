using System;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;

namespace AlphaCabulary.ApplicationCore.Catalog.Utilities
{
    public class Timer : ITimer
    {
        public event EventHandler<TimerEventArgs> TimerTickEventHandler;
        private bool _isRunning;

        public async Task StartAsync(int numSeconds)
        {
            _isRunning = true;

            while (numSeconds >= 0 && _isRunning)
            {
                TimerTickEventHandler?.Invoke(this, new TimerEventArgs(numSeconds));

                await Task.Delay(1000); // one second tick

                --numSeconds;
            }
        }

        public void Stop()
        {
            _isRunning = false;
        }
    }
}