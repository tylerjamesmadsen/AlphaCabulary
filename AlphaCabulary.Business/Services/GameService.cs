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
        public event EventHandler<LetterPairEventArgs> LetterPairGenerationEventHandler;
        public event EventHandler<TimerEventArgs> GameTimerTickEventHandler;
        public event EventHandler<GameScoreEventArgs> GameScoreCalculationEventHandler;

        public bool IsRunning { get; private set; }

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

        public void Start(int numSeconds, int numPairs)
        {
            IsRunning = true;

            InitializeLetterPairs(numPairs);
            _timer.StartAsync(numSeconds);
        }

        private void OnTimerTick(object sender, TimerEventArgs e)
        {
            GameTimerTickEventHandler?.Invoke(this, e);
        }

        private void InitializeLetterPairs(int numPairs)
        {
            var letterPairs = new List<string>(numPairs);

            while (numPairs > 0)
            {
                letterPairs.Add(_letterPairGenerator.GetLetterPair());
                --numPairs;
            }

            LetterPairGenerationEventHandler?.Invoke(this, new LetterPairEventArgs(letterPairs));
        }

        public void Stop(bool isCancelled)
        {
            IsRunning = false;

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

            GameScoreCalculationEventHandler?.Invoke(this, new GameScoreEventArgs(scores));
        }
    }
}