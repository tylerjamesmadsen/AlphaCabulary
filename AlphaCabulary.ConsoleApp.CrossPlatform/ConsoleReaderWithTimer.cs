using System;
using System.Threading;

namespace AlphaCabulary.CLI.CrossPlatform
{
    public static class ConsoleReaderWithTimer
    {
        private static readonly AutoResetEvent _getInput;
        private static readonly AutoResetEvent _gotInput;
        private static string input;

        static ConsoleReaderWithTimer()
        {
            _getInput = new AutoResetEvent(false);
            _gotInput = new AutoResetEvent(false);
            var inputThread = new Thread(reader) {IsBackground = true};

            inputThread.Start();
        }

        private static void reader()
        {
            while (true)
            {
                _getInput.WaitOne();
                input = Console.ReadLine();
                _gotInput.Set();
            }
        }

        // omit the parameter to read a line without a timeout
        public static string ReadLine(int timeOutMilliseconds = Timeout.Infinite)
        {
            _getInput.Set();
            bool success = _gotInput.WaitOne(timeOutMilliseconds);
            if (success)
            {
                return input;
            }
            else
            {
                throw new TimeoutException("User did not provide input within the time limit.");
            }
        }

        public static bool TryReadLine(out string line, int timeOutMilliseconds = Timeout.Infinite)
        {
            _getInput.Set();
            bool success = _gotInput.WaitOne(timeOutMilliseconds);
            line = success ? input : null;
            return success;
        }
    }
}