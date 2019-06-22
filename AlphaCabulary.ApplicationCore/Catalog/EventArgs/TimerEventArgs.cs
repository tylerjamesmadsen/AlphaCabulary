namespace AlphaCabulary.ApplicationCore.Catalog.EventArgs
{
    public class TimerEventArgs : System.EventArgs
    {
        public int Seconds;
        public int Minutes => Seconds % 60;

        public TimerEventArgs(int seconds)
        {
            Seconds = seconds;
        }
    }
}