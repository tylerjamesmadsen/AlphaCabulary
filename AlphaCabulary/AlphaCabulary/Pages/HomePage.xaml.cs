using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlphaCabulary.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnPlayButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }

        private async void OnInstructionsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new InstructionsPage());
        }

        private async void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GameSettingsPage());
        }

        private async void OnHighScoresButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HighScoresPage());
        }
    }
}