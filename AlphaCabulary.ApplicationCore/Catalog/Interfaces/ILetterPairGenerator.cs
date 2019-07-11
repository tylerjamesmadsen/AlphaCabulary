using System.Collections.Generic;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface ILetterPairGenerator
    {
        string GetLetterPair();
        string GetLetterPair(int index);
        IList<string> GetLetterPairList(int numPairs);
    }
}
