using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        private void OnStartStopButtonClicked(object sender, EventArgs e)
        {
            if (!_gameService.IsRunning)
            {
                _gameService.Stop(true);
                return;
            }

            _gameService.Start();
        }
    }
}