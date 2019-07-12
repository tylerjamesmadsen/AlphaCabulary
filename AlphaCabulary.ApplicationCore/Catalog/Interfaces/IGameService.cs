﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IGameService
    {
        event EventHandler<LetterPairsEventArgs> LetterPairsGenerated;
        event EventHandler<TimerEventArgs> TimerTicked;
        event EventHandler<GameScoreEventArgs> ScoreCalculated;
        event EventHandler<System.EventArgs> GameStarted;
        event EventHandler<System.EventArgs> GameFinished;
        event EventHandler<System.EventArgs> GameCancelled;

        void StartCancel();
        void Stop();
        Task CalculateScoresAsync();
        void UpdateUserWordEntry(string letterPair, string userEntry);
    }
}