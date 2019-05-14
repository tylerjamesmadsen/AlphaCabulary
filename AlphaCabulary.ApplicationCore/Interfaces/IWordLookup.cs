using AlphaCabulary.ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaCabulary.ApplicationCore.Interfaces
{
    public interface IWordLookup
    {
        Task<IList<WordDefinitionSyllableCount>> GetWordDefinitionSyllableCountAsync(string word);
    }
}
