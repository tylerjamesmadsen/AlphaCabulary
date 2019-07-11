using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.ApplicationCore.Catalog.Models;

namespace AlphaCabulary.Business.Services
{
    public class GameService : IGameService
    {
        public event EventHandler<LetterPairsEventArgs> LetterPairsGenerated;
        public event EventHandler<TimerEventArgs> TimerTicked;
        public event EventHandler<GameScoreEventArgs> ScoreCalculated;
        public event EventHandler<EventArgs> GameStarted;
        public event EventHandler<EventArgs> GameFinished;
        public event EventHandler<EventArgs> GameCancelled;

        private bool _isRunning;

        private readonly ILetterPairGenerator _letterPairGenerator;
        private readonly ITimer _timer;
        private readonly IScoreCalculator _scoreCalculator;

        public GameService(ILetterPairGenerator letterPairGenerator, ITimer timer, IScoreCalculator scoreCalculator)
        {
            _letterPairGenerator = letterPairGenerator ?? throw new ArgumentNullException(nameof(letterPairGenerator));
            _timer = timer ?? throw new ArgumentNullException(nameof(timer));
            _scoreCalculator = scoreCalculator ?? throw new ArgumentException(nameof(scoreCalculator));

            SubscribeToCustomEvents();
        }

        ~GameService()
        {
            UnsubscribeFromCustomEvents();
        }

        private void SubscribeToCustomEvents()
        {
            _timer.TimerTickEventHandler += OnTimerTick;
        }

        private void UnsubscribeFromCustomEvents()
        {
            _timer.TimerTickEventHandler -= OnTimerTick;
        }

        public void StartCancel()
        {
            if (_isRunning)
            {
                Cancel();
                return;
            }

            _isRunning = !_isRunning;

            Start();
        }

        private void Start()
        {
            GameStarted?.Invoke(this, EventArgs.Empty);

            IList<string> letterPairs = _letterPairGenerator.GetLetterPairList(4);
            LetterPairsGenerated?.Invoke(this, new LetterPairsEventArgs(letterPairs)); // TODO: use numPairs from settings
            _timer.StartAsync(10); // TODO: use time from settings
        }

        private void Cancel()
        {
            Stop();
            GameCancelled?.Invoke(this, EventArgs.Empty);
            return;
        }

        private void OnTimerTick(object sender, TimerEventArgs e)
        {
            TimerTicked?.Invoke(this, e);

            if (e.TotalSeconds != 0) { return; }

            Stop();
            GameFinished?.Invoke(this, EventArgs.Empty);
        }

        public void Stop()
        {
            _isRunning = false;

            _timer.Stop();
        }

        public async Task CalculateScoresAsync(IList<string> words)
        {
            var scores = new List<Score>();

            foreach (string word in words)
            {
                Score score = await _scoreCalculator.CalculateScoreAsync(word);
                scores.Add(score);
            }

            ScoreCalculated?.Invoke(this, new GameScoreEventArgs(scores));
        }
    }
}