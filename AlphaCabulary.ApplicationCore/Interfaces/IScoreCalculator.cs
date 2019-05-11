using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCabulary.ApplicationCore.Interfaces
{
    public interface IScoreCalculator
    {
        int CalculateScore(string word);
    }
}
