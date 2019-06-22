using System;
using System.Collections.Generic;
using System.Linq;
using AlphaCabulary.ApplicationCore.Catalog.Interfaces;

namespace AlphaCabulary.Business.Game
{
    public class MultipleInternalLetterPairGenerator
    {
        private readonly ILetterPairGenerator _generator;
        private const int MAX_TRIES = 3;

        public MultipleInternalLetterPairGenerator(ILetterPairGenerator generator)
        {
            _generator = generator ?? throw new ArgumentException(nameof(generator));
        }

        /// <summary>
        /// Generates the letter pairs.
        /// </summary>
        /// <param name="pairGenerator"></param>
        /// <param name="numPairs"></param>
        /// <returns>An IEnumerable of the string type.</returns>
        public IEnumerable<string> GenerateMultipleLetterPairs(int numPairs)
        {
            if (numPairs == 1)
            {
                return new List<string> { _generator.GetLetterPair() };
            }

            var pairs = new string[numPairs];

            for (var i = 0; i < pairs.Length; i++)
            {
                string pair = _generator.GetLetterPair();

                var numTries = 0;
                while (pairs.Contains(pair) && numTries < MAX_TRIES)
                {
                    pair = _generator.GetLetterPair();
                    ++numTries;
                }

                pairs[i] = pair;
            }

            return pairs;
        }
    }
}