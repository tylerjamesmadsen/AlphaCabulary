using AlphaCabulary.Business.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlphaCabulary.Business.Test.Game.ScoreCalculatorTest
{
    [TestClass]
    public class ScoreCalculatorShould
    {
        [TestMethod]
        public void CalculateScoreForOneSyllableWordWithNoExtraPoints()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateWordScore("toe");

            //-- Assert
            var expected = 3; // 3
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreForOneSyllableWordWith2ExtraPoints()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateWordScore("HaT");

            //-- Assert
            var expected = 4; // 3 + 1
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreForOneSyllableWordWith3ExtraPoints()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            //var actual = calculator.CalculateScore(TODO);

            //-- Assert
            //var expected = TODO;
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreForOneSyllableWordWith1And2And3ExtraPoints()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            //var actual = calculator.CalculateScore(TODO);

            //-- Assert
            //var expected = TODO;
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreForOneSyllableWordWith1DoubleAPair()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            //var actual = calculator.CalculateScore(TODO);

            //-- Assert
            //var expected = TODO;
            //Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreForOneSyllableWordWith2DoubleAPairs()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            //var actual = calculator.CalculateScore(TODO);

            //-- Assert
            //var expected = TODO;
            //Assert.AreEqual(expected, actual);
        }
    }
}
