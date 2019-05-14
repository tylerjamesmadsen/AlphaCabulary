using AlphaCabulary.ApplicationCore.Models;
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
            const string WORD = null;

            //-- Act
            Score score = await calculator.CalculateScoreAsync(WORD);
            int actual = (int)score?.Total;

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreForEmptyStringAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "";

            //-- Act
            Score score = await calculator.CalculateScoreAsync(WORD);
            int actual = (int)score?.Total;

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreForWhiteSpaceAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "      ";

            //-- Act
            Score score = await calculator.CalculateScoreAsync(WORD);
            int actual = (int)score?.Total;

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }
    }
}
