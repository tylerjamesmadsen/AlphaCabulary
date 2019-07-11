using System.Collections;
using System.Collections.Generic;

namespace AlphaCabulary.ApplicationCore.Catalog.EventArgs
{
    public class LetterPairsEventArgs : System.EventArgs
    {
        public IList<string> LetterPairs { get; }

        public LetterPairsEventArgs(IList<string> letterPairs)
        {
            LetterPairs = letterPairs;
        }
    }
}