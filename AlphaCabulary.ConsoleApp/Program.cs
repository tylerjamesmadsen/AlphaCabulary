using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Timers;
using AlphaCabulary.Business.Game;
using AlphaCabulary.Business.WordLookup;
using AlphaCabulary.ApplicationCore.Models;

namespace AlphaCabulary.ConsoleApp
{
    internal class Program
    {
        [DllImport("User32.Dll", EntryPoint = "PostMessageA")]
        private static extern bool PostMessage(IntPtr hWnd, uint msg, int wParam, int lParam);

        const int VK_RETURN = 0x0D;
        const int WM_KEYDOWN = 0x100;

        /// <summary>
        /// The main entry to the program.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static async Task Main(string[] args)
        {
            DisplayTitle();

            do
            {
                await RunGameAsync();
            } while (PromptForPlayGame());
        }

        /// <summary>
        /// Displays a colorful title.
        /// </summary>
        private static void DisplayTitle()
        {
            Console.Write("#############\n#   ");
            ConsoleHelper.WriteInColor(() => Console.Write("A"), ConsoleColor.Red);
            ConsoleHelper.WriteInColor(() => Console.Write("L"), ConsoleColor.Blue);
            ConsoleHelper.WriteInColor(() => Console.Write("P"), ConsoleColor.Yellow);
            ConsoleHelper.WriteInColor(() => Console.Write("H"), ConsoleColor.Green);
            ConsoleHelper.WriteInColor(() => Console.Write("A"), ConsoleColor.Red);
            Console.WriteLine("   #");
            Console.WriteLine("# -cabulary #");
            Console.WriteLine("#############");

        }

        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <returns></returns>
        private static async Task RunGameAsync()
        {
            int numWords = PromptForNumWords();
            int millisecondsPerWord = PromptForSecondsPerWord() * 1000;

            var timer = new Timer(millisecondsPerWord);
            timer.Elapsed += (sender, args) =>
            {
                ConsoleHelper.WriteInColor(() => Console.WriteLine("\nTimed out."), ConsoleColor.Red);
                IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
                PostMessage(hWnd, WM_KEYDOWN, VK_RETURN, 0);
            };

            const string MESSAGE = "\nComplete each word by adding letters to the provided pair." +
                                   "\n\nPress any key to start...";

            Console.Write(MESSAGE);
            Console.ReadKey();
            Console.WriteLine("\n");

            var pairGenerator = new InternalLetterPairGenerator();
            IEnumerable<string> pairs = new PairGenerationFactory(pairGenerator).GenerateMultipleLetterPairs(numWords);
            var scores = new List<Score>();
            var wordLookup = new DatamuseWordLookup();
            var scoreCalculator = new ScoreCalculator(wordLookup);

            foreach (string pair in pairs)
            {
                ConsoleHelper.WriteInColor(() => Console.Write(pair), ConsoleColor.Yellow);

                timer.Start();
                string word = pair + ConsoleHelper.ReadInColor(Console.ReadLine, ConsoleColor.Cyan)?.Trim();
                Score score = await scoreCalculator.CalculateScoreAsync(word);

                scores.Add(score);
            }

            timer.Stop();

            DisplayResults(scores);
        }

        private void SendEnterToConsole()
        {
            IntPtr hWnd = Process.GetCurrentProcess().MainWindowHandle;
            PostMessage(hWnd, WM_KEYDOWN, VK_RETURN, 0);
        }

        /// <summary>
        /// Prompts for the number of seconds per word.
        /// </summary>
        /// <returns></returns>
        private static int PromptForSecondsPerWord()
        {
            Console.Write("\nHow many seconds per word? ");
            var seconds = 0;

            while (seconds < 5)
            {
                string response = ConsoleHelper.ReadInColor(Console.ReadLine, ConsoleColor.Cyan);

                if (!int.TryParse(response, out seconds))
                {
                    Console.Write("Please enter a valid number: ");
                    continue;
                }

                if (seconds < 5)
                {
                    Console.WriteLine("The minimum is 5 seconds.");
                    Console.Write("Please enter a valid number: ");
                }
            }

            return seconds;
        }

        /// <summary>
        /// Displays the results of the game.
        /// </summary>
        /// <param name="scores"></param>
        private static void DisplayResults(IEnumerable<Score> scores)
        {
            var totalScore = 0;

            foreach (Score score in scores)
            {
                totalScore += score.Total;
                Console.WriteLine();
                DisplayScore(score);
            }

            Console.Write("\nTotal score: ");
            ConsoleHelper.WriteInColor(() => Console.WriteLine(totalScore), ConsoleColor.Magenta);
        }

        /// <summary>
        /// Asks the user if they want to play again.
        /// </summary>
        /// <returns>A boolean value.</returns>
        private static bool PromptForPlayGame()
        {
            Console.Write("\nPlay again? (Y/N) ");

            ConsoleKey? response = null;

            while (response != ConsoleKey.Y && response != ConsoleKey.N)
            {
                response = ConsoleHelper.ReadInColor(() => Console.ReadKey().Key, ConsoleColor.Cyan);

                if (response == ConsoleKey.Y)
                {
                    return true;
                }

                if (response != ConsoleKey.N)
                {
                    ConsoleHelper.WriteInColor(() => Console.Write("\nPlease enter a valid response (Y/N): "), ConsoleColor.Red);
                }
            }

            return false;
        }



        /// <summary>
        /// Displays the score with color formatting.
        /// </summary>
        /// <param name="score"></param>
        private static void DisplayScore(Score score)
        {
            Console.Write("Score for ");
            ConsoleHelper.WriteInColor(() => Console.Write($"\"{score.Word}\""), ConsoleColor.Yellow);
            Console.Write(": ");
            ConsoleHelper.WriteInColor(() => Console.Write($"{score.Total}"), score.Total > 0 ? ConsoleColor.Green : ConsoleColor.Gray);

            if (score.Total > 0)
            {
                Console.Write("\n   Breakdown:");
                Console.Write("\n      Points per letter: ");
                ConsoleHelper.WriteInColor(() => Console.Write(score.PointsPerLetter), ConsoleColor.Blue);
                Console.Write("\n      Extra points: ");
                ConsoleHelper.WriteInColor(() => Console.Write(score.ExtraPoints), ConsoleColor.Blue);
                Console.Write("\n      Syllable points: ");
                ConsoleHelper.WriteInColor(() => Console.Write(score.SyllablePoints), ConsoleColor.Blue);
                Console.Write("\n      Double letter points: ");
                ConsoleHelper.WriteInColor(() => Console.Write(score.DoubleLetterPoints), ConsoleColor.Blue);
                Console.WriteLine();
            }
            else
            {
                Console.Write("\n      Reason: ");
                ConsoleHelper.WriteInColor(() => Console.Write(score.ZeroScoreReason), ConsoleColor.Red);
                Console.WriteLine();
            }
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
                string response = ConsoleHelper.ReadInColor(Console.ReadLine, ConsoleColor.Cyan);

                if (!int.TryParse(response, out numWords))
                {
                    ConsoleHelper.WriteInColor(() => Console.Write("Please enter a valid number: "), ConsoleColor.Red);
                    continue;
                }

                if (numWords <= 0)
                {
                    ConsoleHelper.WriteInColor(() => Console.Write("Please enter a number greater than 0: "), ConsoleColor.Red);
                }
            }

            return numWords;
        }
    }
}
