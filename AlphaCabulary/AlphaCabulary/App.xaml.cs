using System;
using System.IO;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.Business.Factories;
using AlphaCabulary.Data.LocalDb;
using AlphaCabulary.Pages;
using Xamarin.Forms;

namespace AlphaCabulary
{
    public partial class App : Application
    {
        private static NoteDatabase _database;

        public static NoteDatabase Database =>
            _database ?? (_database = new NoteDatabase(Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3")));

        public App()
        {
            InitializeComponent();

            var letterPairGeneratorFactory = new InternalLetterPairGeneratorFactory();
            var timerServiceFactory = new TimerFactory();
            IGameService gameService = new GameServiceFactory(letterPairGeneratorFactory, timerServiceFactory).Create();

            MainPage = new NavigationPage(new HomePage(gameService));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}