using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.EventArgs;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlphaCabulary.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamePage : ContentPage
    {
        private readonly IGameService _gameService;

        public GamePage(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));

            InitializeComponent();
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

        private void SubscribeToCustomEvents()
        {
            _gameService.TimerTicked += OnGameTimerTick;
            _gameService.LetterPairsGenerated += OnLetterPairsGenerated;
            _gameService.ScoreCalculated += OnGameScoreCalculated;
            _gameService.GameStarted += OnGameStarted;
            _gameService.GameFinished += OnGameFinishedAsync;
            _gameService.GameFinished += OnGameStopped;
            _gameService.GameCancelled += OnGameCancelled;
            _gameService.GameCancelled += OnGameStopped;
        }

        private void UnsubscribeFromCustomEvents()
        {
            _gameService.TimerTicked -= OnGameTimerTick;
            _gameService.LetterPairsGenerated -= OnLetterPairsGenerated;
            _gameService.ScoreCalculated -= OnGameScoreCalculated;
            _gameService.GameStarted -= OnGameStarted;
            _gameService.GameFinished -= OnGameFinishedAsync;
            _gameService.GameFinished -= OnGameStopped;
            _gameService.GameCancelled -= OnGameCancelled;
            _gameService.GameCancelled -= OnGameStopped;
        }

        private void OnGameTimerTick(object sender, TimerEventArgs e)
        {
            TimerLabel.Text = $"Time Remaining: {e}";
        }

        private void OnLetterPairsGenerated(object sender, LetterPairsEventArgs e)
        {
            LetterPair1.Text = e.LetterPairs[0];
            LetterPair2.Text = e.LetterPairs[1];
            LetterPair3.Text = e.LetterPairs[2];
            LetterPair4.Text = e.LetterPairs[3];
        }

        private async void OnGameFinishedAsync(object sender, EventArgs e)
        {
            var words = new List<string>
            {
                LetterPair1.Text + UserEntryEditor1.Text,
                LetterPair2.Text + UserEntryEditor2.Text,
                LetterPair3.Text + UserEntryEditor3.Text,
                LetterPair4.Text + UserEntryEditor4.Text
            };

            await _gameService.CalculateScoresAsync();
        }

        private void OnGameScoreCalculated(object sender, GameScoreEventArgs e)
        {
            WordScore1.Text += e.Scores[0].WordScore;
            WordScore2.Text += e.Scores[1].WordScore;
            WordScore3.Text += e.Scores[2].WordScore;
            WordScore4.Text += e.Scores[3].WordScore;

            WordScore1.IsVisible = true;
            WordScore2.IsVisible = true;
            WordScore3.IsVisible = true;
            WordScore4.IsVisible = true;

            TotalScore.Text = e.TotalScore.ToString();
            TotalScore.IsVisible = true;
            TotalScoreLabel.IsVisible = true;
        }

        private void StartStopButton_OnClicked(object sender, EventArgs e)
        {
            _gameService.StartCancel();
        }

        private void OnGameStarted(object sender, EventArgs e)
        {
            StartStopButton.Text = "Stop";
            StartStopButton.BackgroundColor = (Color)Application.Current.Resources["AlphacabularyRed"];
        }

        private void OnGameStopped(object sender, EventArgs e)
        {
            StartStopButton.Text = "Start!";
            StartStopButton.BackgroundColor = (Color)Application.Current.Resources["AlphacabularyGreen"];
        }

        private void OnGameCancelled(object sender, EventArgs e)
        {
            _gameService.Stop();
            Reset();
        }

        private void Reset()
        {
            UserEntryEditor1.Text = "";
            UserEntryEditor2.Text = "";
            UserEntryEditor3.Text = "";
            UserEntryEditor4.Text = "";
        }

        private void UserEntryEditor_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _gameService.UpdateWordDictionary((sender as Editor)?.Text, e.NewTextValue);
        }
    }
}