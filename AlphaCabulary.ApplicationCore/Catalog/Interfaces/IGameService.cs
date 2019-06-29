using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IGameService
    {
        event EventHandler<LetterPairEventArgs> LetterPairGenerationEventHandler;
        event EventHandler<TimerEventArgs> GameTimerTickEventHandler;
        event EventHandler<GameScoreEventArgs> GameScoreCalculationEventHandler;

        bool IsRunning { get; }

        void Start(int numSeconds, int numPairs);
        void Stop(bool isCancelled);
        Task CalculateScoresAsync(IList<string> words);
    }
}