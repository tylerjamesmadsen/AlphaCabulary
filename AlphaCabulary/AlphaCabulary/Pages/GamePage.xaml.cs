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

            _gameService.Stop(true);

            UnsubscribeFromCustomEvents();
        }

        private void SubscribeToCustomEvents()
        {
            _gameService.GameTimerTickEventHandler += OnGameTimerTick;
            _gameService.LetterPairGenerationEventHandler += OnLetterPairsGenerated;
            _gameService.GameScoreCalculationEventHandler += OnGameScoreCalculated;
        }

        private void UnsubscribeFromCustomEvents()
        {
            _gameService.GameTimerTickEventHandler -= OnGameTimerTick;
            _gameService.LetterPairGenerationEventHandler -= OnLetterPairsGenerated;
            _gameService.GameScoreCalculationEventHandler -= OnGameScoreCalculated;
        }

        private async void OnGameTimerTick(object sender, TimerEventArgs e)
        {
            TimerLabel.Text = $"Time Remaining: {e}";

            if (e.TotalSeconds != 0)
            {
                return;
            }

            var words = new List<string>
            {
                LetterPair0.Text + UserEntryEditor0.Text,
                LetterPair1.Text + UserEntryEditor1.Text,
                LetterPair2.Text + UserEntryEditor2.Text,
                LetterPair3.Text + UserEntryEditor3.Text
            };

            await _gameService.CalculateScoresAsync(words);
        }

        private void OnLetterPairsGenerated(object sender, LetterPairEventArgs e)
        {
            LetterPair0.Text = e.LetterPairs[0];
            LetterPair1.Text = e.LetterPairs[1];
            LetterPair2.Text = e.LetterPairs[2];
            LetterPair3.Text = e.LetterPairs[3];
        }

        private void OnGameScoreCalculated(object sender, GameScoreEventArgs e)
        {
            WordScore0.Text += e.Scores[0].WordScore;
            WordScore1.Text += e.Scores[1].WordScore;
            WordScore2.Text += e.Scores[2].WordScore;
            WordScore3.Text += e.Scores[3].WordScore;

            WordScore0.IsVisible = true;
            WordScore1.IsVisible = true;
            WordScore2.IsVisible = true;
            WordScore3.IsVisible = true;

            TotalScore.Text = (e.Scores[0].WordScore + e.Scores[1].WordScore + e.Scores[2].WordScore + e.Scores[3].WordScore).ToString();
            TotalScore.IsVisible = true;
            TotalScoreLabel.IsVisible = true;
        }

        private void StartStopButton_OnClicked(object sender, EventArgs e)
        {
            if (_gameService.IsRunning)
            {
                _gameService.Stop(true);
                ToggleStartStopButtonStyle(false);

                return;
            }

            ToggleStartStopButtonStyle(true);
            // TODO: use time and numPairs from settings
            _gameService.Start(10, 4);
        }

        private void ToggleStartStopButtonStyle(bool isStopButton)
        {
            if (isStopButton)
            {
                StartStopButton.Text = "Stop";
                StartStopButton.BackgroundColor = (Color)Application.Current.Resources["AlphacabularyRed"];
                return;
            }

            StartStopButton.Text = "Start!";
            StartStopButton.BackgroundColor = (Color)Application.Current.Resources["AlphacabularyGreen"];
        }
    }
}