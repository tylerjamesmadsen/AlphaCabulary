using System;

namespace AlphaCabulary.ConsoleApp.CrossPlatform
{
    public static class ConsoleHelper
    {
        /// <summary>
        /// Writes a message to the console using a specified text color.
        /// </summary>
        /// <param name="writeAction"></param>
        /// <param name="color"></param>
        internal static void WriteInColor(Action writeAction, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            writeAction();
            Console.ResetColor();
        }

        /// <summary>
        /// Reads from the Console using a specified color for the user's text.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="readFunc"></param>
        /// <param name="color"></param>
        /// <returns>The specified type.</returns>
        internal static T ReadInColor<T>(Func<T> readFunc, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            T response = readFunc();
            Console.ResetColor();

            return response;
        }
    }
}