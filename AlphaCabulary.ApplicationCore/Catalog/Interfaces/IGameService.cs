using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IGameService
    {
        event EventHandler<LetterPairEventArgs> LetterPairsGeneratedEventHandler;
        event EventHandler<TimerEventArgs> GameTimerTickedEventHandler;
        event EventHandler<GameScoreEventArgs> GameScoreCalculatedEventHandler;

        bool IsRunning { get; }

        void Start(int numSeconds, int numPairs);
        void Stop(bool isCancelled);
        Task CalculateScoresAsync(IList<string> words);
    }
}