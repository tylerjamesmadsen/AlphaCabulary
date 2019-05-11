using AlphaCabulary.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCabulary.Business.Game
{
    public class ScoreCalculator : IScoreCalculator
    {
        // TODO: move values to config
        private const int POINTS_PER_LETTER = 1;
        private readonly int[] _extraPoints = { 1, 2, 3 };
        private const string EXTRA_POINTS_0 = "aeiot";
        private const string EXTRA_POINTS_1 = "dhlnrs";
        private const string EXTRA_POINTS_2 = "bcfgmpuwy";
        private const string EXTRA_POINTS_3 = "jkqvxz";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public int CalculateWordScore(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) { return 0; }

            var score = CalculatePointsPerLetter(word);
            score += CalculateExtraPoints(word);

            return score;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private int CalculatePointsPerLetter(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) { return 0; }

            return word.Length;
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

            foreach(var letter in word)
            {
                if (EXTRA_POINTS_0.Contains(letter.ToString().ToLower()))
                {
                    // no extra points
                    continue;
                }

                if (EXTRA_POINTS_1.Contains(letter.ToString().ToLower()))
                {
                    extraPointsScore += _extraPoints[0];
                    continue;
                }

                if (EXTRA_POINTS_2.Contains(letter.ToString().ToLower()))
                {
                    extraPointsScore += _extraPoints[1];
                    continue;
                }

                if (EXTRA_POINTS_3.Contains(letter.ToString().ToLower()))
                {
                    extraPointsScore += _extraPoints[2];
                    continue;
                }
            }

            return extraPointsScore;
        }
    }
}
