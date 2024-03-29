﻿using System.Collections.Generic;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IScoreSettings
    {
        int PointsPerLetter { get; set; }
        IDictionary<char, int> LetterSpecificPoints { get; set; }
        int PointsPerSyllable { get; set; }
        int PointsPerDoubleLetter { get; set; }
    }
}