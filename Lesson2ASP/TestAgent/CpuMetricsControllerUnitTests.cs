
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
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.Create(It.IsAny<CpuMetric>())).Verifiable();

            // ��������� �������� �� �����������
            IActionResult result = controller.Create(new MetricAgent.Requests.CpuMetricCreateRequest { Time = TimeSpan.FromSeconds(1), Value = 50 });
            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.Create(It.IsAny<CpuMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Create_ShouldCall_GetMetrics_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Verifiable();

            // ��������� �������� �� �����������
            var result = controller.GetMetrics(new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 50));


            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.Once());
        }
        [Fact]
        public void Create_ShouldCall_GetAll_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.GetAll());

            // ��������� �������� �� �����������
            var result = controller.GetAll();


            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.GetAll());
        }

    }
}



