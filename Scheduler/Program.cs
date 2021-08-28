using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler
{
    class Program
    {
        //bool daysRemaining(Project project, DateTime date)
        //{

        //}
        static void Main(string[] args)
        {
            var cml = new Project("CMyLife", 100, new DateTime(2021, 07, 05), 400);
            var beijer = new Project("Beijer", 50, new DateTime(2021, 07, 05));
            var jesper = new Person("Jesper", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 8, 0, 0 }));
            jesper.DynamicAllocations[cml] = 1.0;
            var stuart = new Person("Stuart", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 0, 0, 0 }));
            stuart.FixedAllocations[beijer] = 8;
            stuart.DynamicAllocations[cml] = 1.0;
            var projects = new Project[] { cml, beijer };
            var persons = new Person[] { stuart, jesper };

            DateTime date = projects.Min(project => project.StartDate);
            var schedule = new Dictionary<DateTime, Dictionary<Person, Dictionary<Project, double>>>();
            double amountOfWork = 400;
            var workToDistribute = new Dictionary<Person, Dictionary<Project, int>>();
            while (amountOfWork > 0)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    foreach(var person in persons)
                    {
                        var hoursAvailable = person.WorkPattern.Pattern.Sum(); // TODO, compute this specific week availability
                        foreach(var allocatedProject in person.FixedAllocations.Keys.OrderBy(project => project.Priority))
                        {
                            var allocatedHours = person.FixedAllocations[allocatedProject];
                            var grantedHours = Math.Min(hoursAvailable, allocatedHours);
                            // TODO distribute hours
                            hoursAvailable -= grantedHours;
                            Console.WriteLine($"Starting on {date} {person.Name} will work {grantedHours} on {allocatedProject.Name}");
                        }
                        foreach(var allocatedProject in person.DynamicAllocations.Keys.OrderBy(project => project.Priority))
                        {
                            var allocatedHours = hoursAvailable * person.DynamicAllocations[allocatedProject];
                            // TODO distribute hours
                            amountOfWork -= allocatedHours; // TODO, decrease work of the project
                            Console.WriteLine($"Starting on {date} {person.Name} will work {allocatedHours} on {allocatedProject.Name}");
                        }
                    }
                }
                date = date.AddDays(1);
            }
        }
    }
}
