using AlphaCabulary.ApplicationCore.Catalog.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaCabulary.Data.API
{
    // https://www.datamuse.com/api/

    public interface IDatamuseApi
    {
        [Get("/words?sp={word}&md=dps")]
        Task<IList<WordDefinitionsSyllablesPartsOfSpeech>> GetDefinitionsSyllablesPartsOfSpeech(string word);
    }
}
