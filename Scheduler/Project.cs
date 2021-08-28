using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    public class Project
    {
        public Project(string name, int priority, DateTime startDate)
        {
            Name = name;
            Priority = priority;
            StartDate = startDate;
        }
        public Project(string name, int priority, DateTime startDate, DateTime endDate)
            : this(name, priority, startDate)
        {
            EndDate = endDate;
        }
        public Project(string name, int priority, DateTime startDate, int work)
            : this(name, priority, startDate)
        {
            Work = work;
        }
        public string Name { get; set; }
        public int Priority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Work { get; set; }
    }
}
