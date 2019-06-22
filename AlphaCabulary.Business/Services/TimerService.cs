using AlphaCabulary.ApplicationCore.Catalog.Interfaces;

namespace AlphaCabulary.Business.Services
{
    public class TimerService : ITimerService
    {
        private readonly ITimer _timer;

        public TimerService(ITimer timer)
        {
            _timer = timer;
        }

        public void Start(int numSeconds)
        {
            _timer.Start(numSeconds);
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}