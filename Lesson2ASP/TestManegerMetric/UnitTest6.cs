using System;
using Lesson2ASP;
using Lesson2ASP.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;


namespace TestManegerMetric
{
    public class AgentsControllerUnitTests
    {
        private AgentsController controller;

        public AgentsControllerUnitTests()
        {
            controller = new AgentsController();
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


