using System;
using System.Collections.Generic;
using System.Linq;
using AlphaCabulary.ApplicationCore.Catalog.Models;

namespace AlphaCabulary.ApplicationCore.Catalog.EventArgs
{
    public class GameScoreEventArgs
    {
        public IList<Score> Scores { get; }
        public int TotalScore { get; }

        public GameScoreEventArgs(IList<Score> scores)
        {
            Scores = scores ?? throw new ArgumentNullException(nameof(scores));

            foreach (Score score in scores)
            {
                TotalScore += score.WordScore;
            }
        }
    }
}