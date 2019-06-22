using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.ApplicationCore.Catalog.Models;
using AlphaCabulary.Data.API;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaCabulary.Business.WordLookup
{
    public class DatamuseWordLookup : IWordLookup
    {
        private static readonly IDatamuseApi _api = RestService.For<IDatamuseApi>("https://api.datamuse.com");

        public Task<IList<WordDefinitionsSyllablesPartsOfSpeech>> GetWordDefinitionSyllableCountAsync(string word)
        {
            return _api.GetDefinitionsSyllablesPartsOfSpeech(word);
        }
    }
}
