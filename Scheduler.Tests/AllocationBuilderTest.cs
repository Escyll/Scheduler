using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Scheduler.Tests
{
    [TestClass]
    public class AllocationBuilderTest
    {
        [TestMethod]
        public void StartAndEndDate()
        {
            // Arrange
            Project expectedProject = new Project("Dummy", 100, DateTime.Now);
            double expectedPercentage = 1.0;
            DateTime expectedStart = DateTime.Now;
            DateTime expectedEnd = DateTime.Now.AddMonths(1);

            // Act
            Allocation allocation = new AllocationBuilder().OnProject(expectedProject)
                                                           .ForPercentage(expectedPercentage)
                                                           .StartingOn(expectedStart)
                                                           .Until(expectedEnd)
                                                           .Build();
            // Assert
            Assert.AreEqual(expectedProject, allocation.Project);
            Assert.AreEqual(expectedPercentage, allocation.Percentage);
            Assert.AreEqual(expectedStart, allocation.StartDate);
            Assert.AreEqual(expectedEnd, allocation.EndDate);
        }

        [TestMethod]
        public void AfterAllocationAndEndDate()
        {
            // Arrange
            Allocation otherAllocation = new Allocation { };
            Project expectedProject = new Project("Dummy", 100, DateTime.Now);
            double expectedPercentage = 1.0;
            DateTime expectedEnd = DateTime.Now.AddMonths(1);

            // Act
            Allocation allocation = new AllocationBuilder().OnProject(expectedProject)
                                                           .ForPercentage(expectedPercentage)
                                                           .After(otherAllocation)
                                                           .Until(expectedEnd)
                                                           .Build();
            // Assert
            Assert.AreEqual(expectedProject, allocation.Project);
            Assert.AreEqual(expectedPercentage, allocation.Percentage);
            Assert.AreEqual(otherAllocation, allocation.AfterAllocation);
            Assert.AreEqual(expectedEnd, allocation.EndDate);
        }

        [TestMethod]
        public void StartAndUntilAllocation()
        {
            // Arrange
            Allocation otherAllocation = new Allocation { };
            Project expectedProject = new Project("Dummy", 100, DateTime.Now);
            double expectedPercentage = 1.0;
            DateTime expectedStart = DateTime.Now;
            DateTime expectedEnd = DateTime.Now.AddMonths(1);

            // Act
            Allocation allocation = new AllocationBuilder().OnProject(expectedProject)
                                                           .ForPercentage(expectedPercentage)
                                                           .StartingOn(expectedStart)
                                                           .Until(otherAllocation)
                                                           .Build();
            // Assert
            Assert.AreEqual(expectedProject, allocation.Project);
            Assert.AreEqual(expectedPercentage, allocation.Percentage);
            Assert.AreEqual(expectedStart, allocation.StartDate);
            Assert.AreEqual(otherAllocation, allocation.UntilAllocation);
        }

        [TestMethod]
        public void StartAndUntilFinished()
        {
            // Arrange
            Project expectedProject = new Project("Dummy", 100, DateTime.Now);
            double expectedPercentage = 1.0;
            DateTime expectedStart = DateTime.Now;

            // Act
            Allocation allocation = new AllocationBuilder().OnProject(expectedProject)
                                                           .ForPercentage(expectedPercentage)
                                                           .StartingOn(expectedStart)
                                                           .UntilFinished()
                                                           .Build();
            // Assert
            Assert.AreEqual(expectedProject, allocation.Project);
            Assert.AreEqual(expectedPercentage, allocation.Percentage);
            Assert.AreEqual(expectedStart, allocation.StartDate);
            Assert.IsTrue(allocation.UntilFinished);
        }

        [TestMethod]
        public void Invalid()
        {
            Assert.ThrowsException<InvalidOperationException>(() => new AllocationBuilder().ForPercentage(1.0)
                                                                                           .StartingOn(DateTime.Now)
                                                                                           .UntilFinished()
                                                                                           .Build());
            Assert.ThrowsException<InvalidOperationException>(() => new AllocationBuilder().OnProject(new Project("Dummy", 100, DateTime.Now))
                                                                                           .StartingOn(DateTime.Now)
                                                                                           .UntilFinished()
                                                                                           .Build());
            Assert.ThrowsException<InvalidOperationException>(() => new AllocationBuilder().OnProject(new Project("Dummy", 100, DateTime.Now))
                                                                                           .ForPercentage(1.0)
                                                                                           .StartingOn(DateTime.Now)
                                                                                           .Build());
        }
    }
}
