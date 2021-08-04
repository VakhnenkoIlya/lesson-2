
using MetricAgent.Controllers;
using MetricsAgent;
using MetricsAgent.DAL;
using MetricAgent.Requests;
using MetricAgent.Responses;
using MetricAgent.DAL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricAgent;

namespace TestAgent
{
    public class DotNetMetricsControllerUnitTests
    {
        private readonly DotNetMetricsController controller;
        private readonly Mock<IDotNetMetricsRepository> mockRepository;
        private readonly Mock<ILogger<DotNetMetricsController>> mockLogger;
       

        public DotNetMetricsControllerUnitTests()
        {
            mockRepository = new Mock<IDotNetMetricsRepository>();
            mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            controller = new DotNetMetricsController(mockLogger.Object, mockRepository.Object);
        }

        [Fact]
        public void Create_ShouldCall_GetEror_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит  объект
            mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.GetEror(new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 50));


            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.Once());
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mockRepository.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.Create(new DotNetMetricCreateRequest { Time = TimeSpan.FromSeconds(1).ToString(), Value = 50 }); 


            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mockRepository.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }
    }
}
