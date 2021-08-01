
using MetricAgent;
using MetricAgent.Controllers;
using MetricsAgent.DAL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using MetricAgent.Requests;

namespace TestAgent
{
    public class NetworkMetricsControllerUnitTests
    {
        private readonly NetworkMetricsController controller;
        private readonly Mock<INetworkMetricsRepository> mockRepository;
        private readonly Mock<ILogger<NetworkMetricsController>> mockLogger;


        public NetworkMetricsControllerUnitTests()
        {
            mockRepository = new Mock<INetworkMetricsRepository>();
            mockLogger = new Mock<ILogger<NetworkMetricsController>>();
            controller = new NetworkMetricsController(mockLogger.Object, mockRepository.Object);
        }


        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mockRepository.Setup(repository => repository.Create(It.IsAny<NetworkMetric>())).Verifiable();

            // выполняем действие на контроллере
            IActionResult result = controller.Create(new NetworkMetricCreateRequest { Time = TimeSpan.FromSeconds(1).ToString(), Value = 50 });
            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mockRepository.Verify(repository => repository.Create(It.IsAny<NetworkMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Create_ShouldCall_GetNetwork_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.GetNetwork(new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 50));


            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.Once());
        }
    }
}

