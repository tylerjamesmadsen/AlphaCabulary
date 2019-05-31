using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Interfaces;
using AlphaCabulary.ApplicationCore.Models;

namespace AlphaCabulary.Business.Game
{
    public class ScoreCalculator : IScoreCalculator
    {
        private readonly IWordLookup _wordLookup;

        // TODO: move values to config
        private const int POINTS_PER_LETTER = 1;
        private readonly int[] _extraPoints = { 1, 2, 3 };
        private const string EXTRA_POINTS_0 = "AEIOT";
        private const string EXTRA_POINTS_1 = "DHLNRS";
        private const string EXTRA_POINTS_2 = "BCFGMPUWY";
        private const string EXTRA_POINTS_3 = "JKQVXZ";

        public ScoreCalculator(IWordLookup wordLookup)
        {
            _wordLookup = wordLookup ?? throw new ArgumentNullException(nameof(wordLookup));
        }

        /// <summary>
        /// Calculates total word score
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<Score> CalculateScoreAsync(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) { return new Score(word); }

            word = word.Trim().ToUpper();
            IList<WordDefinitionsSyllablesPartsOfSpeech> result = await _wordLookup.GetWordDefinitionSyllableCountAsync(word);
            WordDefinitionsSyllablesPartsOfSpeech firstResult = result?.FirstOrDefault();

            if (word != firstResult?.Word?.ToUpper())
            {
                // not found in dictionary
                return new Score(word);
            }

            if (IsProperNoun(firstResult.PartsOfSpeech))
            {
                return new Score(word);
            }

            var pointsPerLetter = CalculatePointsPerLetter(word);
            var extraPoints = CalculateExtraPoints(word);
            var syllablePoints = (int)firstResult?.NumSyllables;
            var doubleLetterPoints = CalculateDoubleLetterPoints(word);

            var score = new Score(word, pointsPerLetter, extraPoints, syllablePoints, doubleLetterPoints, 0);

            return score;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static bool IsProperNoun(IList<string> partsOfSpeech)
        {
            if (partsOfSpeech is null) { throw new ArgumentNullException(nameof(partsOfSpeech)); }

            return partsOfSpeech.Contains("prop");
        }

        /// <summary>
        /// Calculates number of points per letter
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static int CalculatePointsPerLetter(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) { return 0; }

            return word.Trim().Length * POINTS_PER_LETTER;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int CalculateExtraPoints(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) { return 0; }

            var extraPointsScore = 0;

            foreach (var letter in word.ToUpper().Trim())
            {
                if (EXTRA_POINTS_0.Contains(letter.ToString()))
                {
                    // no extra points
                    continue;
                }

                if (EXTRA_POINTS_1.Contains(letter.ToString()))
                {
                    extraPointsScore += _extraPoints[0];
                    continue;
                }

                if (EXTRA_POINTS_2.Contains(letter.ToString()))
                {
                    extraPointsScore += _extraPoints[1];
                    continue;
                }

                if (EXTRA_POINTS_3.Contains(letter.ToString()))
                {
                    extraPointsScore += _extraPoints[2];
                }
            }

            return extraPointsScore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private static int CalculateDoubleLetterPoints(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) { return 0; }

            var doubleLetterScore = 0;
            word = word.Trim().ToUpper();

            for (var i = 0; i < word.Length - 1; i++)
            {
                if (word[i] == word[i + 1])
                {
                    ++doubleLetterScore;
                }
            }

            return doubleLetterScore;
        }
    }
}
