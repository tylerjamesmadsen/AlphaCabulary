using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.Factories;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlphaCabulary.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private readonly IGameService _gameService;
        private readonly WordGridFactory _wordGridFactory;

        public GamePage(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            _wordGridFactory = new WordGridFactory(_gameService);

            InitializeComponent();
            AddWordGrids();
        }

        protected override void OnAppearing()
        {
            SubscribeToCustomEvents();
        }

        protected override void OnDisappearing()
        {
            // TODO: confirm cancel/exit

            _gameService.Stop();

            UnsubscribeFromCustomEvents();
        }

        private void AddWordGrids()
        {
            for (var i = 0; i < 4 /*TODO: use value from settings*/; i++)
            {
                UserEntryStackLayout.Children.Add(_wordGridFactory.Create());
            }
        }

        private void SubscribeToCustomEvents()
        {
            _gameService.TimerTicked += OnGameTimerTick;
            _gameService.LetterPairsGenerated += OnLetterPairsGenerated;
            _gameService.ScoreCalculated += OnGameScoreCalculated;
            _gameService.GameStarted += OnGameStarted;
            _gameService.GameFinished += OnGameFinishedAsync;
            _gameService.GameCancelled += OnGameCancelled;
        }

        private void UnsubscribeFromCustomEvents()
        {
            _gameService.TimerTicked -= OnGameTimerTick;
            _gameService.LetterPairsGenerated -= OnLetterPairsGenerated;
            _gameService.ScoreCalculated -= OnGameScoreCalculated;
            _gameService.GameStarted -= OnGameStarted;
            _gameService.GameFinished -= OnGameFinishedAsync;
            _gameService.GameCancelled -= OnGameCancelled;
        }

        private void OnGameTimerTick(object sender, TimerEventArgs e)
        {
            TimerLabel.Text = $"Time Remaining: {e}";
        }

        private void OnLetterPairsGenerated(object sender, LetterPairsEventArgs e)
        {
            _wordGridFactory.UpdateLetterPairs(e.LetterPairs);
        }

        private async void OnGameFinishedAsync(object sender, EventArgs e)
        {
            OnGameStopped(sender, e);
            await _gameService.CalculateScoresAsync();
        }

        private void OnGameScoreCalculated(object sender, GameScoreEventArgs e)
        {
            _wordGridFactory.UpdateWordScores(e.Scores);

            TotalScoreLabel.Text = $"Total: {e.TotalScore}" ;
            TotalScoreLabel.IsVisible = true;
        }

        private void StartStopButton_OnClicked(object sender, EventArgs e)
        {
            ResetUI();
            _gameService.StartCancel();
        }

        private void OnGameStarted(object sender, EventArgs e)
        {
            _wordGridFactory.SetUserEntryReadOnlyState(false);

            StartStopButton.Text = "Stop";
            StartStopButton.BackgroundColor = (Color)Application.Current.Resources["AlphacabularyRed"];
        }

        private void OnGameStopped(object sender, EventArgs e)
        {
            _wordGridFactory.SetUserEntryReadOnlyState(true);

            StartStopButton.Text = "Start!";
            StartStopButton.BackgroundColor = (Color)Application.Current.Resources["AlphacabularyGreen"];
        }

        private void OnGameCancelled(object sender, EventArgs e)
        {
            OnGameStopped(sender, e);
            ResetUI();
        }

        private void ResetUI()
        {
            _wordGridFactory.Reset();

            TotalScoreLabel.IsVisible = false;
            TotalScoreLabel.Text = "";
        }
    }
}