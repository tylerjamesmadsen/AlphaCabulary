using AlphaCabulary.ApplicationCore.Interfaces;
using AlphaCabulary.ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlphaCabulary.Business.Game
{
    public class ScoreCalculator : IScoreCalculator
    {
        private readonly IWordLookup _wordLookup;

        // TODO: move values to config
        private const int POINTS_PER_LETTER = 1;
        private readonly int[] _extraPoints = { 1, 2, 3 };
        private const string EXTRA_POINTS_0 = "aeiot";
        private const string EXTRA_POINTS_1 = "dhlnrs";
        private const string EXTRA_POINTS_2 = "bcfgmpuwy";
        private const string EXTRA_POINTS_3 = "jkqvxz";

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
            if (string.IsNullOrWhiteSpace(word)) { return new Score(); }

            word = word.Trim().ToLower();
            IList<WordDefinitionSyllableCount> result = await _wordLookup.GetWordDefinitionSyllableCountAsync(word);
            var firstResult = result?.FirstOrDefault();

            if (word != firstResult?.Word?.ToLower())
            {
                return new Score();
            }

            int pointsPerLetter = CalculatePointsPerLetter(word);
            int extraPoints = CalculateExtraPoints(word);
            int syllablePoints = (int)firstResult?.NumSyllables;
            int doubleLetterPoints = CalculateDoubleLetterPoints(word);

            var score = new Score(pointsPerLetter, extraPoints, syllablePoints, doubleLetterPoints, 0);

            return score;
        }

        /// <summary>
        /// Calculates number of points per letter
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int CalculatePointsPerLetter(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) { return 0; }

            return word.Trim().Length;
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

            foreach (var letter in word.ToLower().Trim())
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
                    continue;
                }
            }

            return extraPointsScore;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int CalculateDoubleLetterPoints(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) { return 0; }

            var doubleLetterScore = 0;
            word = word.Trim().ToLower();

            for (int i = 0; i < word.Length - 1; i++)
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
