using System.Collections.Generic;
using AlphaCabulary.ApplicationCore.Catalog.Models;

namespace AlphaCabulary.ApplicationCore.Catalog.EventArgs
{
    public class GameScoreEventArgs
    {
        public IList<Score> Scores { get; }

        public GameScoreEventArgs(IList<Score> scores)
        {
            Scores = scores;
        }
    }
}