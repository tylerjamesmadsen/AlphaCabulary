using AlphaCabulary.ApplicationCore.Catalog.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IWordLookup
    {
        Task<IList<WordDefinitionsSyllablesPartsOfSpeech>> GetWordDefinitionSyllableCountAsync(string word);
    }
}
