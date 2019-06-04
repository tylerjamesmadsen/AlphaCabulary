using System;
using System.Collections.Generic;
using AlphaCabulary.ApplicationCore.Interfaces;

namespace AlphaCabulary.Business.Game
{
    public class PairGenerationFactory
    {
        private readonly ILetterPairGenerator _generator;

        public PairGenerationFactory(ILetterPairGenerator generator)
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
            var pairs = new string[numPairs];

            for (var i = 0; i < pairs.Length; i++)
            {
                string pair = _generator.GetLetterPair();

                if (i > 0)
                {
                    while (pairs[i - 1] == pair)
                    {
                        pair = _generator.GetLetterPair();
                    }
                }

                pairs[i] = pair;
            }

            return pairs;
        }
    }
}