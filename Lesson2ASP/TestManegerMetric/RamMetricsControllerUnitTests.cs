using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Lesson2ASP.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestManegerMetric
{
    public class RamMetricsControllerUnitTests
    {
        private readonly RamMetricsController controller;

        private readonly Mock<ILogger<RamMetricsController>> mockLogger;
        public RamMetricsControllerUnitTests()
        {

            mockLogger = new Mock<ILogger<RamMetricsController>>();
            controller = new RamMetricsController(mockLogger.Object);

        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsFromAgent(agentId, fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetMetricsFromAllCluster()
        {
            //Arrange
          
            var fromTime = TimeSpan.FromSeconds(0);
            var toTime = TimeSpan.FromSeconds(100);

            //Act
            var result = controller.GetMetricsFromAllCluster(fromTime, toTime);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}


