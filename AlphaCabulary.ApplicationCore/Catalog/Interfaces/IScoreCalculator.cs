using AlphaCabulary.ApplicationCore.Catalog.Models;
using System.Threading.Tasks;

namespace AlphaCabulary.ApplicationCore.Catalog.Interfaces
{
    public interface IScoreCalculator
    {
        Task<Score> CalculateScoreAsync(string word);
    }
}
