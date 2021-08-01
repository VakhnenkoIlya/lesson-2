using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Lesson2ASP.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TestManegerMetric
{
    public class AgentsControllerUnitTests
    {
        private readonly AgentsController controller;
      
        private readonly Mock<ILogger<AgentsController>> mockLogger;
        public AgentsControllerUnitTests()
        {
            
            mockLogger = new Mock<ILogger<AgentsController>>();
            controller = new AgentsController(mockLogger.Object);

        }

        [Fact]
        public void GetMetricsFromAgent_ReturnsOk()
        {
            //Arrange
            var agentInfo = new AgentInfo();

            //Act
            var result = controller.RegisterAgent(agentInfo);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void EnableAgentById()
        {
            //Arrange
            var agentInfo = new AgentInfo() { AgentId = 1 };

            //Act
            var result = controller.EnableAgentById(agentInfo.AgentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void DisableAgentById()
        {
            //Arrange
            var agentInfo = new AgentInfo() { AgentId = 1 };

            //Act
            var result = controller.DisableAgentById(agentInfo.AgentId);

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }

        [Fact]
        public void ShowAgent()
        {
            //Arrange
          

            //Act
            var result = controller.ShowAgent();

            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}


