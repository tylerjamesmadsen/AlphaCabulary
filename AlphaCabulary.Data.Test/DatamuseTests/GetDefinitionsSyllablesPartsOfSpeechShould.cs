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
    public class GetDefinitionsSyllablesPartsOfSpeechShould
    {
        [TestMethod]
        public async Task GetPartOfSpeechForProperNounAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseApi>("https://api.datamuse.com");

            //-- act
            var result = await api.GetDefinitionsSyllablesPartsOfSpeech("amanda");
            var actual = result.FirstOrDefault()?.PartsOfSpeech;

            //-- assert
            var expected = new List<string> { "n", "prop" };

            for (var i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }

        [TestMethod]
        public async Task Get2SyllableCountAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseApi>("https://api.datamuse.com");

            //-- act
            var result = await api.GetDefinitionsSyllablesPartsOfSpeech("pronounce");
            var actual = result.FirstOrDefault()?.NumSyllables;

            //-- assert
            var expected = 2;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Get5SyllableCountAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseApi>("https://api.datamuse.com");

            //-- act
            var result = await api.GetDefinitionsSyllablesPartsOfSpeech("unpredictable");
            var actual = result.FirstOrDefault()?.NumSyllables;

            //-- assert
            var expected = 5;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Get10SyllableCountAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseApi>("https://api.datamuse.com");

            //-- act
            var result = await api.GetDefinitionsSyllablesPartsOfSpeech("antiestablishmentarianism");
            var actual = result.FirstOrDefault()?.NumSyllables;

            //-- assert
            var expected = 10;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async Task Get14SyllableCountAsync()
        {
            //-- arrange
            var api = RestService.For<IDatamuseApi>("https://api.datamuse.com");

            //-- act
            var result = await api.GetDefinitionsSyllablesPartsOfSpeech("supercalifragilisticexpialidocious");
            var actual = result.FirstOrDefault()?.NumSyllables;

            //-- assert
            var expected = 14;
            Assert.AreEqual(expected, actual);
        }
    }
}
