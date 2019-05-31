using AlphaCabulary.ApplicationCore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaCabulary.ApplicationCore.Interfaces
{
    public interface IWordLookup
    {
        Task<IList<WordDefinitionSyllablesPartsOfSpeech>> GetWordDefinitionSyllableCountAsync(string word);
    }
}
