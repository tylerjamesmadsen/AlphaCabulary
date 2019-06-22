namespace AlphaCabulary.ApplicationCore.Catalog.Models
{
    public class Score
    {
        public string Word { get; }
        public int PointsPerLetter { get; }
        public int ExtraPoints { get; }
        public int SyllablePoints { get; }
        public int DoubleLetterPoints { get; }
        public int SubjectPoints { get; }
        public int Total => PointsPerLetter + ExtraPoints + SyllablePoints + DoubleLetterPoints + SubjectPoints;
        public string ZeroScoreReason;

        public Score(string word, string zeroScoreReason) : this(word, 0, 0, 0, 0, 0)
        {
            ZeroScoreReason = zeroScoreReason;
        }

        public Score(string word, int pointsPerLetter, int extraPoints, int syllablePoints, int doubleLetterPoints, int subjectPoints)
        {
            Word = word;
            PointsPerLetter = pointsPerLetter;
            ExtraPoints = extraPoints;
            SyllablePoints = syllablePoints;
            DoubleLetterPoints = doubleLetterPoints;
            SubjectPoints = subjectPoints;
        }

        public override string ToString()
        {
            return $"Score for \"{Word}\": {Total}" +
                   (Total > 0
                       ? "\n   Breakdown:" +
                         $"\n      Points per letter: {PointsPerLetter}" +
                         $"\n      Extra points: {ExtraPoints}" +
                         $"\n      Syllable points: {SyllablePoints}" +
                         $"\n      Double letter points: {DoubleLetterPoints}"
                       : $"\n      Reason: {ZeroScoreReason}"
                   );
        }
    }
}
