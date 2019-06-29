using AlphaCabulary.ApplicationCore.Catalog.Models;
using AlphaCabulary.Business.Game;
using AlphaCabulary.Business.WordLookup;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace AlphaCabulary.Business.Test.ScoreCalculatorTests
{
    [TestClass]
    public class ScoreCalculatorFor2SyllablesShould
    {
        private static readonly DatamuseWordLookup _wordLookup = new DatamuseWordLookup();

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
        public async Task CalculateScoreWith2DoublePairsAsync()
        {
            //-- Arrange
            var calculator = new ScoreCalculator(_wordLookup);
            const string WORD = "teepee";

            //-- Act
            Score score = await calculator.CalculateScoreAsync(WORD);
            var actual = (int)score?.WordScore;

            //-- Assert
            var expected = 12; // 6 + (0 + 0 + 0 + 2 + 0 + 0) + 2 + 2
            Assert.AreEqual(expected, actual);
        }
    }
}
