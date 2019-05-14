using AlphaCabulary.ApplicationCore.Interfaces;
using AlphaCabulary.ApplicationCore.Models;
using AlphaCabulary.Data.API;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCabulary.Business.WordLookup
{
    public class DatamuseWordLookup : IWordLookup
    {
        private static IDatamuseAPI _api = RestService.For<IDatamuseAPI>("https://api.datamuse.com");

        public Task<IList<WordDefinitionSyllableCount>> GetWordDefinitionSyllableCountAsync(string word)
        {
            return _api.GetDefinitionsAndSyllableCounts(word);
        }
    }
}
