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
                    new ColumnDefinition { Width = GridLength.Star }
                },
                RowDefinitions = new RowDefinitionCollection { new RowDefinition(), new RowDefinition() },
                //Margin = new Thickness(20.0, 0.0)
            };

            Editor editor = GenerateUserEntryEditor();
            editor.TextChanged += UserEntryEditor_OnTextChanged;
            _gameService.AddIdToWordRepository(editor.Id);
            _userEntryEditors.Add(editor);
            Grid.SetRow(editor, 0);
            wordGrid.Children.Add(editor);

            Label wordScoreLabel = GenerateWordScoreLabel();
            _wordScoreLabels.Add(wordScoreLabel);
            Grid.SetRow(wordScoreLabel, 1);
            wordGrid.Children.Add(wordScoreLabel);

            return wordGrid;
        }

        private void UserEntryEditor_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is Editor editor)) return;
            if (string.IsNullOrWhiteSpace(e.OldTextValue) || e.NewTextValue is null) return;

            if (!string.IsNullOrWhiteSpace(editor.Placeholder) && !e.NewTextValue.StartsWith(editor.Placeholder))
            {
                editor.Text = e.OldTextValue;
                return;
            }

            editor.Text = e.NewTextValue.Sanitize();
            _gameService.UpdateUserWordEntry(editor.Id, e.NewTextValue);
        }

        private static Editor GenerateUserEntryEditor()
        {
            return new Editor
            {
                Keyboard = Keyboard.Text,
                IsTextPredictionEnabled = false,
                IsReadOnly = true
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
                if (_userEntryEditors.Count < i + 1) continue;

                _userEntryEditors[i].Text = letterPairs[i];
                _userEntryEditors[i].Placeholder = letterPairs[i];
                _gameService.UpdateUserWordEntry(_userEntryEditors[i].Id, letterPairs[i]);
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

        public void SetUserEntryReadOnlyState(bool isReadOnly)
        {
            foreach (Editor editor in _userEntryEditors)
            {
                editor.IsReadOnly = isReadOnly;
            }
        }

        public void Reset()
        {
            foreach (Editor editor in _userEntryEditors)
            {
                editor.TextChanged -= UserEntryEditor_OnTextChanged;
                editor.Text = "";
                editor.TextChanged += UserEntryEditor_OnTextChanged;
            }

            foreach (Label label in _wordScoreLabels)
            {
                label.Text = "";
                label.IsVisible = false;
            }
        }
    }
}