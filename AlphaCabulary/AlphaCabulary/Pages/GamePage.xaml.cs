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

            SubscribeToCustomEvents();
        }

        private void SubscribeToCustomEvents()
        {
            _gameService.Timer.TimerTickEventHandler += OnTimerTick;
        }

        private void UnsubscribeFromCustomEvents()
        {
            _gameService.Timer.TimerTickEventHandler -= OnTimerTick;
        }

        private void OnTimerTick(object sender, TimerEventArgs e)
        {
            TimerLabel.Text = $"{e}";
        }

        private void StartStopButton_OnClicked(object sender, EventArgs e)
        {
            if (_gameService.IsRunning)
            {
                _gameService.Stop(true);
                return;
            }

            _gameService.StartAsync(180/*TODO: use time from settings*/);
        }
    }
}