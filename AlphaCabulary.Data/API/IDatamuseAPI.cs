using AlphaCabulary.ApplicationCore.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlphaCabulary.Data.API
{
    // https://www.datamuse.com/api/

    public interface IDatamuseAPI
    {
        [Get("/words?sp={word}&md=ds")]
        Task<IList<WordDefinitionSyllableCount>> GetDefinitionsAndSyllableCounts(string word);
    }
}
