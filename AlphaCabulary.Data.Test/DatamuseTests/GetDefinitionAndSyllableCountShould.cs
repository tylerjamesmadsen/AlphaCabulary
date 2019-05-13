using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlphaCabulary.ApplicationCore.Models;
using AlphaCabulary.Data.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refit;

namespace AlphaCabulary.Data.Test.DatamuseTests
{
    [TestClass]
    public class GetDefinitionAndSyllableCountShould
    {
        [TestMethod]
        public async Task GetDefinitionAndSyllableCountAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseAPI>("https://api.datamuse.com");

            //-- act
            List<WordDefinition> result = await api.GetPossibleDefinitionsAndSyllableCounts("pronounce");
            var actual = result.FirstOrDefault();

            //-- assert
            var expected = ""; // TODO
            Assert.AreEqual(expected, actual);
        }
    }
}
