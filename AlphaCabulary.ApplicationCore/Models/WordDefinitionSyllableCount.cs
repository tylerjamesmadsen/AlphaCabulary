using System.Collections.Generic;

namespace AlphaCabulary.ApplicationCore.Models
{
    public class WordDefinitionSyllableCount
    {
        public string Word { get; }
        public int NumSyllables { get; }
        public IList<string> Defs { get; }

        public WordDefinitionSyllableCount(string word, int numSyllables, IList<string> defs)
        {
            Word = word;
            NumSyllables = numSyllables;
            Defs = defs;
        }
    }
}
