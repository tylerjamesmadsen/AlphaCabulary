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
        public async Task Get2SyllableCountAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseAPI>("https://api.datamuse.com");

            //-- act
            IList<WordDefinitionSyllableCount> result = await api.GetDefinitionsAndSyllableCounts("pronounce");
            var actual = result.FirstOrDefault()?.NumSyllables;

            //-- assert
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Get5SyllableCountAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseAPI>("https://api.datamuse.com");

            //-- act
            IList<WordDefinitionSyllableCount> result = await api.GetDefinitionsAndSyllableCounts("unpredictable");
            var actual = result.FirstOrDefault()?.NumSyllables;

            //-- assert
            var expected =5;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Get10SyllableCountAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseAPI>("https://api.datamuse.com");

            //-- act
            IList<WordDefinitionSyllableCount> result = await api.GetDefinitionsAndSyllableCounts("antiestablishmentarianism");
            var actual = result.FirstOrDefault()?.NumSyllables;

            //-- assert
            var expected = 10;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Get14SyllableCountAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseAPI>("https://api.datamuse.com");

            //-- act
            IList<WordDefinitionSyllableCount> result = await api.GetDefinitionsAndSyllableCounts("supercalifragilisticexpialidocious");
            var actual = result.FirstOrDefault()?.NumSyllables;

            //-- assert
            var expected = 14;
            Assert.AreEqual(expected, actual);
        }
    }
}
