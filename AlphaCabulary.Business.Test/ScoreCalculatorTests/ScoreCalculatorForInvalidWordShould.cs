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
        public async Task CalculateScoreForProperNounAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "Amanda";

            //-- Act
            var score = await calculator.CalculateScoreAsync(WORD);
            var actual = score?.Total;

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreForNullWordAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = null;

            //-- Act
            var score = await calculator.CalculateScoreAsync(WORD);
            var actual = (int)score?.Total;

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
            var score = await calculator.CalculateScoreAsync(WORD);
            var actual = (int)score?.Total;

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
            var score = await calculator.CalculateScoreAsync(WORD);
            var actual = (int)score?.Total;

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreForNonDictionaryWordAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "afdghophasg";

            //-- Act
            var score = await calculator.CalculateScoreAsync(WORD);
            var actual = (int)score?.Total;

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }
    }
}
