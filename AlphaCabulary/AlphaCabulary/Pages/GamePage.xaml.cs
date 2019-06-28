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
            _gameService.Timer.TimerTickEventHandler += OnTimerTick;
            _gameService.LetterPairsGeneratedEventHandler += OnLetterPairsGenerated;
        }

        private void UnsubscribeFromCustomEvents()
        {
            _gameService.Timer.TimerTickEventHandler -= OnTimerTick;
            _gameService.LetterPairsGeneratedEventHandler -= OnLetterPairsGenerated;
        }

        private void OnTimerTick(object sender, TimerEventArgs e)
        {
            TimerLabel.Text = $"Time Remaining: {e}";
        }

        private void OnLetterPairsGenerated(object sender, LetterPairEventArgs e)
        {
            LetterPair0.Text = e.LetterPairs[0];
            LetterPair1.Text = e.LetterPairs[1];
            LetterPair2.Text = e.LetterPairs[2];
            LetterPair3.Text = e.LetterPairs[3];
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
            _gameService.Start(180, 4);
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