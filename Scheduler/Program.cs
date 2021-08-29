using System;
using System.Collections.Generic;
using System.Linq;

namespace Scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            var cml = new Project("CMyLife", 100, new DateTime(2021, 07, 05), 400);
            var beijer = new Project("Beijer", 50, new DateTime(2021, 07, 05));
            var arne = new Project("Arne", 50, new DateTime(2021, 07, 05));
            var toucan = new Project("Toucan", 50, new DateTime(2021, 07, 05));

            var jesper = new Person("Jesper", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 8, 0, 0 }));
            var stuart = new Person("Stuart", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 0, 0, 0 }));
            var dennis = new Person("Dennis", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 8, 0, 0, 8, 8, 8, 8, 0, 0, 0 }));
            var elviro = new Person("Elviro", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 8, 0, 0 }));
            jesper.Allocations[cml] = 1.0;
            stuart.Allocations[beijer] = 0.25;
            stuart.Allocations[cml] = 0.375;
            stuart.Allocations[arne] = 0.375;
            dennis.Allocations[arne] = 0.5;
            dennis.Allocations[toucan] = 0.5;
            elviro.Allocations[toucan] = 1.0;

            var persons = new Person[] { stuart, jesper, dennis, elviro };
            var projects = new Project[] { cml, beijer, arne, toucan };

            DateTime date = projects.Min(project => project.StartDate);
            var schedule = new Dictionary<DateTime, Dictionary<Person, Dictionary<Project, double>>>();
            double amountOfWork = 1000;
            var workToDistribute = new Dictionary<Person, Dictionary<Project, int>>();
            while (amountOfWork > 0)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    foreach(var person in persons)
                    {
                        var hoursAvailable = Scheduling.AvailableHours(person.WorkPattern, date);
                        foreach(var allocatedProject in person.Allocations.Keys.OrderBy(project => project.Priority))
                        {
                            var allocatedHours = hoursAvailable * person.Allocations[allocatedProject];
                            amountOfWork -= allocatedHours; // TODO, decrease work of the project
                            Console.WriteLine($"Starting on {date.ToString("dd/MM/yyyy")} {person.Name} will work {allocatedHours} on {allocatedProject.Name}");
                        }
                    }
                }
                date = date.AddDays(1);
            }
        }
    }
}
