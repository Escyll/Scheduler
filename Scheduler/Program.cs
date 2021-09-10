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
            //var arne = new Project("Arne", 50, new DateTime(2021, 07, 05));
            //var toucan = new Project("Toucan", 50, new DateTime(2021, 07, 05));

            var jesper = new Person("Jesper", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 8, 0, 0 }));
            var stuart = new Person("Stuart", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 0, 0, 0 }));
            //var dennis = new Person("Dennis", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 8, 0, 0, 8, 8, 8, 8, 0, 0, 0 }));
            //var elviro = new Person("Elviro", new WorkPattern(new DateTime(2021, 07, 05), new int[] { 8, 8, 8, 8, 8, 0, 0 }));

            var stuartBeijer = new AllocationBuilder().OnProject(beijer)
                                                      .StartingOn(beijer.StartDate)
                                                      .Until(beijer.StartDate.AddYears(3))
                                                      .ForPercentage(0.25)
                                                      .Build();
            var stuartCml = new AllocationBuilder().OnProject(cml)
                                                   .StartingOn(cml.StartDate)
                                                   .UntilFinished()
                                                   .ForPercentage(0.75)
                                                   .Build();
            var jesperCml = new AllocationBuilder().OnProject(cml)
                                                   .StartingOn(cml.StartDate)
                                                   .UntilFinished()
                                                   .ForPercentage(0.6)
                                                   .Build();
            stuart.Allocations.Add(stuartBeijer);
            stuart.Allocations.Add(stuartCml);
            jesper.Allocations.Add(jesperCml);

            var persons = new Person[] {
                stuart,
                jesper,
                //dennis,
                //elviro,
            };
            var projects = new Project[] {
                cml,
                beijer,
                //arne,
                //toucan,
            };

            DateTime date = projects.Min(project => project.StartDate);
            var workToDistribute = new Dictionary<Person, Dictionary<Project, int>>();
            int amountOfWork = 1000;
            while (amountOfWork > 0)
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                {
                    foreach (var person in persons)
                    {
                        var hoursAvailable = Scheduling.AvailableHours(person.WorkPattern, date);
                        foreach (var allocatedProject in person.Allocations.Keys.OrderBy(project => project.Priority))
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
