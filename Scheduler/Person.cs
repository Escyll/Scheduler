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
        public Dictionary<Project, double> Allocations { get; set; } = new Dictionary<Project, double>();
    }
}
