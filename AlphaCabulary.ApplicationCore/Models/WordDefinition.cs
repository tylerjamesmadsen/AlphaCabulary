using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCabulary.ApplicationCore.Models
{
    public class WordDefinition
    {
        public string Word { get; }
        public int Score { get; }
        public int NumSyllables { get; }
        public IList<string> Defs { get; }
        public string DefHeadword { get; }

        public WordDefinition(string word, int score, int numSyllables, IList<string> defs, string defHeadword)
        {
            Word = word;
            Score = score;
            NumSyllables = numSyllables;
            Defs = defs;
            DefHeadword = defHeadword;
        }
    }
}
