using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduler
{
    public class Person
    {
        public Person(string name, WorkPattern workPattern)
        {
            Name = name;
            WorkPattern = workPattern;
        }

        public string Name { get; set; }
        public WorkPattern WorkPattern { get; set; }
        public Dictionary<Project, int> FixedAllocations { get; set; } = new Dictionary<Project, int>();
        public Dictionary<Project, double> DynamicAllocations { get; set; } = new Dictionary<Project, double>();
    }
}
