using System.IO;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlphaCabulary.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InstructionsPage : ContentPage
    {
        public InstructionsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            Assembly assembly = typeof(InstructionsPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("AlphaCabulary.Resources.alphacabulary_rules.txt");

            if (stream is null) return;

            string text;
            using (var reader = new StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }

            InstructionsDisplay.Text = text;
        }
    }
}