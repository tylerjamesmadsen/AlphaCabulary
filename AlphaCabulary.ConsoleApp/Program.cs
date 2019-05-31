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
        /// <summary>
        /// The main entry to the program.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static async Task Main(string[] args)
        {
            const string TITLE = "#################\n" +
                                 "# AlphaCabulary #\n" +
                                 "#################";

            Console.WriteLine($"{TITLE}");

            var playGame = true;

            while (playGame)
            {
                await RunGameAsync();

                playGame = PromptForPlayGame();
            }
        }

        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <returns></returns>
        private static async Task RunGameAsync()
        {
            var numWords = PromptForNumWords();

            const string MESSAGE = "\nComplete each word by adding letters to the provided pair." +
                                   "\nPress any key to start...";

            Console.Write(MESSAGE);
            Console.ReadKey();
            Console.WriteLine("\n");

            var pairGenerator = new InternalLetterPairGenerator();
            var pairs = GenerateMultipleLetterPairs(pairGenerator, numWords);
            var scores = new List<Score>();
            var wordLookup = new DatamuseWordLookup();
            var scoreCalculator = new ScoreCalculator(wordLookup);

            foreach (var pair in pairs)
            {
                Console.Write(pair);

                var userEntry = pair + Console.ReadLine()?.Trim();
                var score = await scoreCalculator.CalculateScoreAsync(userEntry);

                scores.Add(score);
            }

            DisplayResults(scores);
        }

        /// <summary>
        /// Displays the results of the game.
        /// </summary>
        /// <param name="scores"></param>
        private static void DisplayResults(IEnumerable<Score> scores)
        {
            var totalScore = 0;

            foreach (var score in scores)
            {
                totalScore += score.Total;
                Console.WriteLine(score.ToString());
            }

            Console.WriteLine($"\nTotal score: {totalScore}");
        }

        /// <summary>
        /// Asks the user if they want to play again.
        /// </summary>
        /// <returns>A boolean value.</returns>
        private static bool PromptForPlayGame()
        {
            Console.Write("\nPlay again? (Y/N) ");

            var response = "";

            while (response != "Y" && response != "N")
            {
                response = Console.ReadLine()?.ToUpper();

                if (response == "Y")
                {
                    return true;
                }

                if (response?.ToUpper() != "N")
                {
                    Console.Write("Please enter a valid response (Y/N): ");
                } 
            }

            return false;
        }

        /// <summary>
        /// Prompts the user for the number of words to be played.
        /// </summary>
        /// <returns>An integer.</returns>
        private static int PromptForNumWords()
        {
            Console.Write("\nHow many words? ");
            var numWords = 0;

            while (numWords < 1)
            {
                var response = Console.ReadLine();

                if (!int.TryParse(response, out numWords))
                {
                    Console.Write("Please enter a valid number: ");
                    continue;
                }

                if (numWords <= 0)
                {
                    Console.Write("Please enter a number greater than 0: ");
                }
            }

            return numWords;
        }

        /// <summary>
        /// Generates the letter pairs.
        /// </summary>
        /// <param name="pairGenerator"></param>
        /// <param name="numPairs"></param>
        /// <returns>A IEnumerable of the string type.</returns>
        private static IEnumerable<string> GenerateMultipleLetterPairs(ILetterPairGenerator pairGenerator, int numPairs)
        {
            var pairs = new string[numPairs];

            for (var i = 0; i < pairs.Length; i++)
            {
                var pair = pairGenerator.GetLetterPair();

                if (i > 0)
                {
                    while (pairs[i - 1] == pair)
                    {
                        pair = pairGenerator.GetLetterPair();
                    }
                }

                pairs[i] = pair;
            }

            return pairs;
        }
    }
}
