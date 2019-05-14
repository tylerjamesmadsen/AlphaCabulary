﻿using AlphaCabulary.Business.Game;
using AlphaCabulary.Business.WordLookup;
using AlphaCabulary.Data.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refit;
using System.Threading.Tasks;

namespace AlphaCabulary.Business.Test.ScoreCalculatorTests
{
    [TestClass]
    public class ScoreCalculatorFor1SyllableShould
    {
        private static readonly DatamuseWordLookup _wordLookup = new DatamuseWordLookup();

        [TestMethod]
        public async Task CalculateScoreWithNoExtraPointsAsync()
        {
            //IDatamuseAPI bob = RestService.For<IDatamuseAPI>("https://api.datamuse.com");

            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "toe";

            //-- Act
            int actual = await calculator.CalculateScoreAsync(WORD);

            //-- Assert
            var expected = 4; // 3 + (0 + 0 + 0) + 1
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreWith2ExtraPointsAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "HaT";

            //-- Act
            int actual = await calculator.CalculateScoreAsync(WORD);

            //-- Assert
            var expected = 5; // 3 + (1 + 0 + 0) + 1
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreWith3ExtraPointsAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "jot";

            //-- Act
            int actual = await calculator.CalculateScoreAsync(WORD);

            //-- Assert
            var expected = 7; // 3 + (3 + 0 + 0) + 1
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreWith1And2And3ExtraPointsAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "Bakes";

            //-- Act
            int actual = await calculator.CalculateScoreAsync(WORD);

            //-- Assert
            var expected = 12; // 5 + (2 + 0 + 3 + 0 + 1) + 1
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task CalculateScoreWith1DoublePairAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "TOO";

            //-- Act
            int actual = await calculator.CalculateScoreAsync(WORD);

            //-- Assert
            var expected = 5; // 3 + (0 + 0 + 0) + 1 + 1
            Assert.AreEqual(expected, actual);
        }
    }
}
