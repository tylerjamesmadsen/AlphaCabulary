using AlphaCabulary.Business.Game;
using AlphaCabulary.Business.WordLookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AlphaCabulary.Business.Test.ScoreCalculatorTests
{
    [TestClass]
    public class ScoreCalculatorForInvalidWordShould
    {
        private static readonly DatamuseWordLookup _wordLookup = new DatamuseWordLookup();

        [TestMethod]
        public async Task CalculateScoreForNullWordAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);

            //-- Act
            int actual = await calculator.CalculateScoreAsync(null);

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreForEmptyStringAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);

            //-- Act
            int actual = await calculator.CalculateScoreAsync("");

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreForWhiteSpaceAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);

            //-- Act
            int actual = await calculator.CalculateScoreAsync("       ");

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }
    }
}
