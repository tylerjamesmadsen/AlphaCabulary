using System.Collections;
using System.Collections.Generic;

namespace AlphaCabulary.ApplicationCore.Catalog.EventArgs
{
    public class LetterPairEventArgs : System.EventArgs
    {
        public IList<string> LetterPairs { get; }

        public LetterPairEventArgs(IList<string> letterPairs)
        {
            LetterPairs = letterPairs;
        }
    }
}