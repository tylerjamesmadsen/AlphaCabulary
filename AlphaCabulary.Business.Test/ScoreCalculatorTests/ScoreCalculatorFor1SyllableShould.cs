using AlphaCabulary.Business.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphaCabulary.Business.Test.ScoreCalculatorTests
{
    [TestClass]
    public class ScoreCalculatorFor1SyllableShould
    {
        [TestMethod]
        public void CalculateScoreWithNoExtraPoints()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateScore("toe");

            //-- Assert
            var expected = 3; // 3 + (0 + 0 + 0)
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreWith2ExtraPoints()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateScore("HaT");

            //-- Assert
            var expected = 4; // 3 + (1 + 0 + 0)
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreWith3ExtraPoints()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateScore("jot");

            //-- Assert
            var expected = 6; // 3 + (3 + 0 + 0)
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreWith1And2And3ExtraPoints()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateScore("Bakes");

            //-- Assert
            var expected = 11; // 5 + (2 + 0 + 3 + 0 + 1)
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CalculateScoreWith1DoublePair()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateScore("TOO");

            //-- Assert
            var expected = 4; // 3 + (0 + 0 + 0) + 1
            Assert.AreEqual(expected, actual);
        }
    }
}
