using System.Collections.Generic;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IMultipleLetterPairGenerator
    {
        IEnumerable<string> GenerateMultipleLetterPairs(int numPairs);
    }
}