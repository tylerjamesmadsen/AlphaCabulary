using AlphaCabulary.ApplicationCore.Models;
using System.Threading.Tasks;

namespace AlphaCabulary.ApplicationCore.Interfaces
{
    public interface IScoreCalculator
    {
        Task<Score> CalculateScoreAsync(string word);
    }
}
