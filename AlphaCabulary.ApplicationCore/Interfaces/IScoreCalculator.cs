using System.Threading.Tasks;

namespace AlphaCabulary.ApplicationCore.Interfaces
{
    public interface IScoreCalculator
    {
        Task<int> CalculateScoreAsync(string word);
    }
}
