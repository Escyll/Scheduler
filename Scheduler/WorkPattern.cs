using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduler
{
    public class WorkPattern
    {
        public WorkPattern(DateTime startTime, int[] pattern)
        {
            StartDate = startTime;
            Pattern = pattern;
        }
        private DateTime _startTime;
        public DateTime StartDate {
            get
            {
                return _startTime;
            }
            set
            {
                if (value.DayOfWeek != DayOfWeek.Monday)
                {
                    throw new NotImplementedException();
                }
                _startTime = value;
            }
        }
        private int[] _pattern;
        public int[] Pattern
        {
            get
            {
                return _pattern;
            }
            set
            {
                if (value.Length <= 0 || value.Length % 7 != 0)
                {
                    throw new NotImplementedException();
                }
                value.All(hours => 0 <= hours && hours <= 8);
                _pattern = value;
            }
        }
    }
}
