﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlphaCabulary.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private readonly IGameService _gameService;

        public HomePage(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            InitializeComponent();
        }

        private async void PlayButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage(_gameService));
        }

        private async void InstructionsButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InstructionsPage());
        }

        private async void SettingsButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameSettingsPage());
        }

        private async void HighScoresButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HighScoresPage());
        }
    }
}