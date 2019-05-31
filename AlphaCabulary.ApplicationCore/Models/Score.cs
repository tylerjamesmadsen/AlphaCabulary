namespace AlphaCabulary.ApplicationCore.Models
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

        public Score(string word) : this(word, 0, 0, 0, 0, 0) { }

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
            var message = $"Score for \"{Word}\": {Total}\n";

            message += Total > 0
                ? "   Breakdown:\n" +
                  $"      Points per letter: {PointsPerLetter}\n" +
                  $"      Extra points: {ExtraPoints}\n" +
                  $"      Syllable points: {SyllablePoints}\n" +
                  $"      Double letter points: {DoubleLetterPoints}\n"
                : $"      Reason: {Word} not found in dictionary.";

            return message;
        }
    }
}
