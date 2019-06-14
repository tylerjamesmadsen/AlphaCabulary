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

        private void OnPlayButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("\"PLAY\" button clicked.");
        }

        private void OnInstructionsButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("\"INSTRUCTIONS\" button clicked.");

        }

        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("\"SETTINGS\" button clicked.");

        }

        private void OnHighScoresButtonClicked(object sender, EventArgs e)
        {
            Debug.WriteLine("\"HIGH SCORES\" button clicked.");

        }
    }
}