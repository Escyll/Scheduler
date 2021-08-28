using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Scheduler.Tests
{
    [TestClass]
    public class WorkPatternTests
    {
        [TestMethod]
        public void WorkPatternTest()
        {
            // Arrange
            var expectedStartDate = new DateTime(2021, 07, 5);
            var expectedPattern = new int[] { 8, 8, 8, 8, 8, 0, 0 };

            // Act
            var workPattern = new WorkPattern(expectedStartDate, expectedPattern);

            // Assert
            Assert.AreEqual(workPattern.StartDate, expectedStartDate);
            Assert.AreEqual(workPattern.Pattern, expectedPattern);
        }

        [TestMethod]
        public void StartDateTest()
        {
            // Arrange
            var expectedStartDate = new DateTime(2021, 07, 12);
            var workPattern = new WorkPattern(new DateTime(2021, 07, 5), new int[] { 0, 0, 0, 0, 0, 0, 0 });

            // Act
            workPattern.StartDate = expectedStartDate;

            // Assert
            Assert.AreEqual(workPattern.StartDate, expectedStartDate);
            Assert.ThrowsException<NotImplementedException>(() => workPattern.StartDate = new DateTime(2021, 07, 13));
        }

        [TestMethod]
        public void PatternTest()
        {
            // Arrange
            var expectedPattern1 = new int[] { 8, 8, 8, 8, 8, 0, 0 };
            var expectedPattern2 = new int[] { 8, 8, 8, 8, 8, 0, 0, 8, 8, 8, 8, 0, 0, 0 };
            var workPattern = new WorkPattern(new DateTime(2021, 07, 5), new int[] { 0, 0, 0, 0, 0, 0, 0 });

            // Act
            workPattern.Pattern = expectedPattern1;

            // Assert
            Assert.AreEqual(workPattern.Pattern, expectedPattern1);

            // Act
            workPattern.Pattern = expectedPattern2;

            // Assert
            Assert.AreEqual(workPattern.Pattern, expectedPattern2);
            Assert.ThrowsException<NotImplementedException>(() => workPattern.Pattern = new int[] { 8, 8, 8, 8, 8 });
        }
    }
}
