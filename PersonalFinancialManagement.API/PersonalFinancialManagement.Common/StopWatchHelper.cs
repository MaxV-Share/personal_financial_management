using System.Diagnostics;

namespace PersonalFinancialManagement.Common
{
    public static class StopWatchHelper
    {
        public static Stopwatch StartStopwatch()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            return sw;
        }

        public static string StopAndReturnElapsedTime(Stopwatch sw)
        {
            sw.Stop();

            var ts = sw.Elapsed;
            var elapsedTime =
                $"{ts.TotalHours:00}:{ts.Minutes:00}:{ts.Seconds:00}:{ts.Milliseconds:00}";

            return elapsedTime;
        }
    }
}
