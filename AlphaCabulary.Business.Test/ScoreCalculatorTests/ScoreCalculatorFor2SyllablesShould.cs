using AlphaCabulary.Business.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphaCabulary.Business.Test.ScoreCalculatorTests
{
    [TestClass]
    public class ScoreCalculatorFor2SyllablesShould
    {
        //[TestMethod]
        //public void CalculateScoreWithNoExtraPoints()
        //{
        //    //-- Arrange
        //    var calculator = new ScoreCalculator();

        //    //-- Act
        //    var actual = calculator.CalculateScore(TODO);

        //    //-- Assert
        //    var expected = TODO;
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod]
        //public void CalculateScoreWith2ExtraPoints()
        //{
        //    //-- Arrange
        //    var calculator = new ScoreCalculator();

        //    //-- Act
        //    var actual = calculator.CalculateScore(TODO);

        //    //-- Assert
        //    var expected = TODO;
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod]
        //public void CalculateScoreWith3ExtraPoints()
        //{
        //    //-- Arrange
        //    var calculator = new ScoreCalculator();

        //    //-- Act
        //    var actual = calculator.CalculateScore(TODO);

        //    //-- Assert
        //    var expected = TODO;
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod]
        //public void CalculateScoreWith1And2And3ExtraPoints()
        //{
        //    //-- Arrange
        //    var calculator = new ScoreCalculator();

        //    //-- Act
        //    var actual = calculator.CalculateScore(TODO);

        //    //-- Assert
        //    var expected = TODO;
        //    Assert.AreEqual(expected, actual);
        //}

        //[TestMethod]
        //public void CalculateScoreWith1DoublePair()
        //{
        //    //-- Arrange
        //    var calculator = new ScoreCalculator();

        //    //-- Act
        //    var actual = calculator.CalculateScore(TODO);

        //    //-- Assert
        //    var expected = TODO;
        //    Assert.AreEqual(expected, actual);
        //}

        [TestMethod]
        public void CalculateScoreWith2DoublePairs()
        {
            //-- Arrange
            var calculator = new ScoreCalculator();

            //-- Act
            var actual = calculator.CalculateScore("teepee");

            //-- Assert
            var expected = 10; // 6 + (0 + 0 + 0 + 2 + 0 + 0) + 2
            Assert.AreEqual(expected, actual);
        }
    }
}
