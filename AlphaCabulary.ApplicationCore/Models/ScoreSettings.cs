using System.Collections.Concurrent;
using System.Collections.Generic;
using AlphaCabulary.ApplicationCore.Interfaces;

namespace AlphaCabulary.ApplicationCore.Models
{
    public class ScoreSettings : IScoreSettings
    {
        public int PointsPerLetter { get; set; }
        public IDictionary<char, int> LetterSpecificPoints { get; set; }
        public int PointsPerSyllable { get; set; }
        public int PointsPerDoubleLetter { get; set; }

        public ScoreSettings()
        {
            PointsPerLetter = 1;
            LetterSpecificPoints = new Dictionary<char, int>
            {
                { 'A', 1},
                { 'B', 1},
                { 'C', 1},
                { 'D', 1},
                { 'E', 1},
                { 'F', 1},
                { 'G', 1},
                { 'H', 1},
                { 'I', 1},
                { 'J', 1},
                { 'K', 1},
                { 'L', 1},
                { 'M', 1},
                { 'N', 1},
                { 'O', 1},
                { 'P', 1},
                { 'Q', 1},
                { 'R', 1},
                { 'S', 1},
                { 'T', 1},
                { 'U', 1},
                { 'V', 1},
                { 'W', 1},
                { 'X', 1},
                { 'Y', 1},
                { 'Z', 1}
            };
            PointsPerSyllable = 1;
            PointsPerDoubleLetter = 1;
        }
    }
}