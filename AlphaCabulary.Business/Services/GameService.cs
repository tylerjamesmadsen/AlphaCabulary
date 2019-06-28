using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;

namespace AlphaCabulary.Business.Services
{
    public class GameService : IGameService
    {
        private readonly ILetterPairGenerator _letterPairGenerator;
        public event EventHandler<LetterPairEventArgs> LetterPairsGeneratedEventHandler;

        public ITimer Timer { get; }
        public bool IsRunning { get; private set; }

        public GameService(ILetterPairGenerator letterPairGenerator, ITimer timerService)
        {
            _letterPairGenerator = letterPairGenerator ?? throw new ArgumentNullException(nameof(letterPairGenerator));
            Timer = timerService ?? throw new ArgumentNullException(nameof(timerService));
        }

        public void Start(int numSeconds, int numPairs)
        {
            IsRunning = true;

            InitializeLetterPairs(numPairs);
            Timer.StartAsync(numSeconds);
        }

        private void InitializeLetterPairs(int numPairs)
        {
            var letterPairs = new List<string>(numPairs);

            while (numPairs > 0)
            {
                letterPairs.Add(_letterPairGenerator.GetLetterPair());
                --numPairs;
            }

            LetterPairsGeneratedEventHandler?.Invoke(this, new LetterPairEventArgs(letterPairs));
        }

        public void Stop(bool isCancelled)
        {
            IsRunning = false;

            Timer.Stop();
        }
    }
}