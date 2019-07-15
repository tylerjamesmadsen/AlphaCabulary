using System;
using System.Collections.Generic;
using AlphaCabulary.ApplicationCore.Catalog.Extensions;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.ApplicationCore.Catalog.Models;
using Xamarin.Forms;

namespace AlphaCabulary.Factories
{
    public class WordGridFactory : IFactory<Grid>
    {
        //private readonly IList<Label> _letterPairLabels = new List<Label>();
        private readonly IList<Label> _wordScoreLabels = new List<Label>();
        private readonly IList<Editor> _userEntryEditors = new List<Editor>();
        private readonly IGameService _gameService;

        public WordGridFactory(IGameService gameService)
        {
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
        }

        public Grid Create()
        {
            var wordGrid = new Grid
            {
                ColumnDefinitions = new ColumnDefinitionCollection {
                    new ColumnDefinition { Width = GridLength.Auto },
                    new ColumnDefinition { Width = GridLength.Star }
                },
                RowDefinitions = new RowDefinitionCollection { new RowDefinition(), new RowDefinition() }
            };

            //Label letterPairLabel = GenerateLetterPairLabel();
            //_letterPairLabels.Add(letterPairLabel);
            //Grid.SetColumn(letterPairLabel, 0);
            //Grid.SetRow(letterPairLabel, 0);
            //wordGrid.Children.Add(letterPairLabel);

            Editor userEntryEditor = GenerateUserEntryEditor();
            _gameService.AddIdToWordRepository(userEntryEditor.Id);
            _userEntryEditors.Add(userEntryEditor);
            Grid.SetColumn(userEntryEditor, 1);
            Grid.SetRow(userEntryEditor, 0);
            wordGrid.Children.Add(userEntryEditor);
            userEntryEditor.TextChanged += delegate (object sender, TextChangedEventArgs e)
            {
                userEntryEditor.Text = e.NewTextValue.Sanitize();
                _gameService.UpdateUserWordEntry(userEntryEditor.Id, /*letterPairLabel.Text + */e.NewTextValue);
            };

            Label wordScoreLabel = GenerateWordScoreLabel();
            _wordScoreLabels.Add(wordScoreLabel);
            Grid.SetColumn(wordScoreLabel, 1);
            Grid.SetRow(wordScoreLabel, 1);
            wordGrid.Children.Add(wordScoreLabel);

            return wordGrid;
        }

        //private static Label GenerateLetterPairLabel()
        //{
        //    return new Label
        //    {
        //        Text = "XX",
        //        VerticalOptions = LayoutOptions.Center,
        //        Margin = new Thickness
        //        {
        //            Bottom = 0.0,
        //            Left = 10.0,
        //            Right = 0.0,
        //            Top = 0.0
        //        }
        //    };
        //}

        private static Editor GenerateUserEntryEditor()
        {
            return new Editor
            {
                Keyboard = Keyboard.Text,
                IsTextPredictionEnabled = false,
                IsEnabled = false
            };
        }

        private static Label GenerateWordScoreLabel()
        {
            return new Label
            {
                IsVisible = false,
            };
        }

        public void UpdateLetterPairs(IList<string> letterPairs)
        {
            for (var i = 0; i < letterPairs.Count; i++)
            {
                //if (_letterPairLabels[i] is null) continue;

                //_letterPairLabels[i].Text = letterPairs[i];

                if (_userEntryEditors[i] is null) continue;

                _userEntryEditors[i].Text = letterPairs[i];
            }
        }

        public void UpdateWordScores(IList<Score> scores)
        {
            for (var i = 0; i < scores?.Count; i++)
            {
                if (i + 1 > _wordScoreLabels.Count) continue;

                _wordScoreLabels[i].Text = $"Word score: {scores[i]?.WordScore ?? 0}";
                _wordScoreLabels[i].IsVisible = true;
            }
        }

        public void SetUserEntryEnabledState(bool isEnabled)
        {
            foreach (Editor editor in _userEntryEditors)
            {
                editor.IsEnabled = isEnabled;
            }
        }

        public void Reset()
        {
            //foreach (Label label in _letterPairLabels)
            //{
            //    label.Text = "XX";
            //}

            foreach (Editor editor in _userEntryEditors)
            {
                editor.Text = "";
            }

            foreach (Label label in _wordScoreLabels)
            {
                label.Text = "";
                label.IsVisible = false;
            }
        }
    }
}