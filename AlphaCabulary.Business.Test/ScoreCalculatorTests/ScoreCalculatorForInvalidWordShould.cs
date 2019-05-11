using System;
using AlphaCabulary.Business.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphaCabulary.Business.Test.ScoreCalculatorTests
{
    [TestClass]
    public class ScoreCalculatorForInvalidWordShould
    {
        [TestMethod]
        public void CalculateScoreForNullWord()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateScore(null);

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreForEmptyString()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateScore("");

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreForWhiteSpace()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateScore("       ");

            //-- Assert
            var expected = 0;
            Assert.AreEqual(expected, actual);
        }
    }
}
