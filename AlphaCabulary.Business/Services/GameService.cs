using System;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;

namespace AlphaCabulary.Business.Services
{
    public class GameService : IGameService
    {
        private readonly ILetterPairGenerator _letterPairGenerator;
        public ITimer Timer { get; }

        public bool IsRunning { get; set; }

        public GameService(ILetterPairGenerator letterPairGenerator, ITimer timerService)
        {
            _letterPairGenerator = letterPairGenerator ?? throw new ArgumentNullException(nameof(letterPairGenerator));
            Timer = timerService ?? throw new ArgumentNullException(nameof(timerService));
        }

        public async Task StartAsync(int numSeconds)
        {


            await Timer.StartAsync(numSeconds);
        }

        public void Stop(bool isCancelled)
        {
            Timer.Stop();
        }
    }
}