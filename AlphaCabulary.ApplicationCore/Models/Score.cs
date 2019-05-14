namespace AlphaCabulary.ApplicationCore.Models
{
    public class Score
    {
        public int PointsPerLetter { get; }
        public int ExtraPoints { get; }
        public int SyllablePoints { get; }
        public int DoubleLetterPoints { get; }
        public int SubjectPoints { get; }
        public int Total => PointsPerLetter + ExtraPoints + SyllablePoints + DoubleLetterPoints + SubjectPoints;

        public Score() : this(0, 0, 0, 0, 0) { }

        public Score(int pointsPerLetter, int extraPoints, int syllablePoints, int doubleLetterPoints, int subjectPoints)
        {
            PointsPerLetter = pointsPerLetter;
            ExtraPoints = extraPoints;
            SyllablePoints = syllablePoints;
            DoubleLetterPoints = doubleLetterPoints;
            SubjectPoints = subjectPoints;
        }
    }
}
