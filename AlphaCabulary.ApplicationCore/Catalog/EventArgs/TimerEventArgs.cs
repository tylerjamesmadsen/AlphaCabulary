namespace AlphaCabulary.ApplicationCore.Catalog.EventArgs
{
    public class TimerEventArgs : System.EventArgs
    {
        public int TotalSeconds { get; }
        public int Seconds { get; }
        public int Minutes { get; }

        public TimerEventArgs(int seconds)
        {
            TotalSeconds = seconds;
            Minutes = seconds / 60;
            Seconds = seconds - Minutes * 60;
        }

        public override string ToString()
        {
            return (Minutes > 9 ? $"{Minutes}" : $"0{Minutes}") + ":" + (Seconds > 9 ? $"{Seconds}" : $"0{Seconds}");
        }
    }
}