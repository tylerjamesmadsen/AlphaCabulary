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
        private static readonly IDatamuseApi _api = RestService.For<IDatamuseApi>("https://api.datamuse.com");

        public Task<IList<WordDefinitionSyllablesPartsOfSpeech>> GetWordDefinitionSyllableCountAsync(string word)
        {
            return _api.GetDefinitionsSyllablesPartsOfSpeech(word);
        }
    }
}
