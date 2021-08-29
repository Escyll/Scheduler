using System;
using System.Linq;

namespace Scheduler
{
    public class Scheduling
    {
        static private bool PatternInstanceContainsDate(DateTime patternStart, int numberOfWeeksInPattern, DateTime date)
        {
            return (date - patternStart).TotalDays < numberOfWeeksInPattern * 7;
        }
        static public int AvailableHours(WorkPattern workPattern, DateTime date)
        {
            var numberOfWeeksInPattern = workPattern.Pattern.Length / 7;
            var currentPatternStart = workPattern.StartDate;
            while (!PatternInstanceContainsDate(currentPatternStart, numberOfWeeksInPattern, date))
            {
                currentPatternStart = currentPatternStart.AddDays(7 * numberOfWeeksInPattern);
            }
            int weekInPattern = (int)Math.Floor((date - currentPatternStart).TotalDays / 7);
            return Enumerable.Range(7 * weekInPattern, 7).Select(x => workPattern.Pattern[x]).Sum();
        }
    }
}
