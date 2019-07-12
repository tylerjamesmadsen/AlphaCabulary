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
        private readonly IList<Label> _letterPairLabels = new List<Label>();
        private readonly IList<Label> _wordScoreLabels = new List<Label>();
        private readonly IList<Editor> _userEntryEditors = new List<Editor>();

        public GamePage(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));

            InitializeComponent();
            BuildWordGrids();
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

        private void BuildWordGrids()
        {
            for (var i = 0; i < 4 /*TODO: use value from settings*/; i++)
            {
                var wordGrid = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Star }
                    },
                    RowDefinitions = new RowDefinitionCollection { new RowDefinition(), new RowDefinition() }
                };

                Label letterPairLabel = GenerateLetterPairLabel();
                _letterPairLabels.Add(letterPairLabel);
                Grid.SetColumn(letterPairLabel, 0);
                Grid.SetRow(letterPairLabel, 0);
                wordGrid.Children.Add(letterPairLabel);

                Editor userEntryEditor = GenerateUserEntryEditor();
                _userEntryEditors.Add(userEntryEditor);
                Grid.SetColumn(userEntryEditor, 1);
                Grid.SetRow(userEntryEditor, 0);
                wordGrid.Children.Add(userEntryEditor);
                userEntryEditor.TextChanged += delegate (object sender, TextChangedEventArgs e)
                {
                    _gameService.UpdateUserWordEntry(letterPairLabel.Text, e.NewTextValue);
                };

                Label wordScoreLabel = GenerateWordScoreLabel();
                _wordScoreLabels.Add(wordScoreLabel);
                Grid.SetColumn(wordScoreLabel, 1);
                Grid.SetRow(wordScoreLabel, 1);
                wordGrid.Children.Add(wordScoreLabel);

                UserEntryStackLayout.Children.Add(wordGrid);
            }
        }

        private static Label GenerateLetterPairLabel()
        {
            return new Label
            {
                Text = "XX",
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness
                {
                    Bottom = 0.0,
                    Left = 10.0,
                    Right = 0.0,
                    Top = 0.0
                }
            };
        }

        private static Editor GenerateUserEntryEditor()
        {
            return new Editor
            {
                Keyboard = Keyboard.Text,
                IsTextPredictionEnabled = false
            };
        }

        private static Label GenerateWordScoreLabel()
        {
            return new Label
            {
                IsVisible = false,
            };
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
            for (var i = 0; i < e.LetterPairs.Count; i++)
            {
                if (_letterPairLabels[i] is null) continue;

                _letterPairLabels[i].Text = e.LetterPairs[i];

                //_userEntryEditors[i].Text = e.LetterPairs[i];
            }
        }

        private async void OnGameFinishedAsync(object sender, EventArgs e)
        {
            await _gameService.CalculateScoresAsync();
        }

        private void OnGameScoreCalculated(object sender, GameScoreEventArgs e)
        {
            for (var i = 0; i < e.Scores.Count; i++)
            {
                if (_wordScoreLabels[i] is null) continue;

                _wordScoreLabels[i].Text = $"Word score: {e.Scores[i].WordScore}";
                _wordScoreLabels[i].IsVisible = true;
            }

            TotalScore.Text = e.TotalScore.ToString();
            TotalScore.IsVisible = true;
            TotalScoreLabel.IsVisible = true;
        }

        private void StartStopButton_OnClicked(object sender, EventArgs e)
        {
            Reset();
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
            foreach (Label label in _letterPairLabels)
            {
                label.Text = "XX";
            }

            foreach (Editor editor in _userEntryEditors)
            {
                editor.Text = "";
            }

            foreach (Label label in _wordScoreLabels)
            {
                label.Text = "";
                label.IsVisible = false;
            }

            TotalScore.Text = "";
            TotalScore.IsVisible = false;
        }
    }
}