using System.Collections.Generic;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;
using AlphaCabulary.Business.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AlphaCabulary.Business.Test.LetterPairsTests
{
    [TestClass]
    public class GetLetterPairFromInternalShould
    {
        private static readonly List<string> _pool = new List<string>
        {
            "AL", "AR", "BA", "BL", "BO", "BR", "BU", "CA", "CH", "CL", "CO", "DE", "DI", "DO", "EN", "EX", "FA",
            "FI", "FL", "FO", "FR", "GA", "IN", "LE", "LO", "MA", "NE", "PA", "PE", "PI", "PL", "PR", "PU", "QU",
            "RA", "RE", "RO", "SE", "SI", "SK", "SO", "ST", "SU", "TA", "TR", "UN", "VI", "VO", "WH", "WI"
        };

        private static readonly ILetterPairGenerator _pairGenerator =
            new InternalLetterPairGenerator();

        [TestMethod]
        public void PullARandomPairFromThePool()
        {
            //-- arrange

            //-- act
            string actual = _pairGenerator.GetLetterPair();

            //-- assert
            Assert.IsTrue(_pool.Contains(actual));
        }

        [TestMethod]
        public void PullASpecificPairFromThePool()
        {
            //-- arrange

            //-- act
            string actual = _pairGenerator.GetLetterPair(0);

            //-- assert
            string expected = _pool[0];
            Assert.AreEqual(expected, actual);
        }
    }
}
