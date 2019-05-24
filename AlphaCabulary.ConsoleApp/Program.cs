using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlphaCabulary.Business.Game;
using AlphaCabulary.Business.WordLookup;
using AlphaCabulary.ApplicationCore.Interfaces;
using AlphaCabulary.ApplicationCore.Models;
using AlphaCabulary.Data.API;

namespace AlphaCabulary.ConsoleApp
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("AlphaCabulary");
            Console.WriteLine("Enter one word per prompt:\n\n");

            InternalLetterPairGenerator pairGenerator = new InternalLetterPairGenerator();
            const int NUM_PAIRS = 4;
            string[] pairs = GenerateMultipleLetterPairs(pairGenerator, NUM_PAIRS);
            //List<string> words = new List<string>();
            List<Score> scores = new List<Score>();
            var wordLookup = new DatamuseWordLookup();
            var scoreCalculator = new ScoreCalculator(wordLookup);

            foreach (var pair in pairs)
            {
                Console.Write(pair);

                var userEntry = pair + Console.ReadLine().Trim();

                var score = await scoreCalculator.CalculateScoreAsync(userEntry);

                scores.Add(score);
            }

            foreach (var score in scores)
            {
                Console.WriteLine(score.Total);
            }

            Console.ReadKey();
        }

        private static string[] GenerateMultipleLetterPairs(ILetterPairGenerator pairGenerator, int numPairs)
        {
            var pairs = new string[numPairs];

            for (int i = 0; i < pairs.Length; i++)
            {
                string pair = pairGenerator.GetLetterPair();

                if (i == 0) { continue; }

                while (pairs[i - 1] == pair)
                {
                    pair = pairGenerator.GetLetterPair();
                }
            }

            return pairs;
        }
    }
}
