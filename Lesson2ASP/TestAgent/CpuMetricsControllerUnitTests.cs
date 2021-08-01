
using MetricAgent;
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

namespace TestAgent
{
    public class CpuMetricsControllerUnitTests
    {
        private readonly CpuMetricsController controller;
        private readonly Mock<ICpuMetricsRepository> mockRepository;
        private readonly Mock<ILogger<CpuMetricsController>> mockLogger;
        public CpuMetricsControllerUnitTests()
        {
            mockRepository = new Mock<ICpuMetricsRepository>();
            mockLogger = new Mock<ILogger<CpuMetricsController>>();
            controller = new CpuMetricsController(mockLogger.Object, mockRepository.Object);

        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // выполняем действие на контроллере
            IActionResult result = controller.Create(new MetricAgent.Requests.CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mockRepository.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Create_ShouldCall_GetMetrics_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Verifiable();

            // выполняем действие на контроллере
            var result = controller.GetMetrics(new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 50));


            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.Once());
        }
        [Fact]
        public void Create_ShouldCall_GetAll_From_Repository()
        {
            // устанавливаем параметр заглушки
            // в заглушке прописываем что в репозиторий прилетит CpuMetric объект
            mockRepository.Setup(repository => repository.GetAll());

            // выполняем действие на контроллере
            var result = controller.GetAll();


            // проверяем заглушку на то, что пока работал контроллер
            // действительно вызвался метод Create репозитория с нужным типом объекта в параметре
            mockRepository.Verify(repository => repository.GetAll());
        }

    }
}



