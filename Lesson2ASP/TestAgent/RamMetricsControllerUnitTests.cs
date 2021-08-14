
using MetricAgent.Controllers;
using MetricsAgent.DAL;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;
using Microsoft.Extensions.Logging;
using MetricAgent.Requests;
using MetricAgent;
using AutoMapper;
using System.Collections.Generic;

namespace TestAgent
{
    public class RamMetricsControllerUnitTests
    {
        private readonly RamMetricsController controller;
        private readonly Mock<IRamMetricsRepository> mockRepository;
        private readonly Mock<ILogger<RamMetricsController>> mockLogger;
        private readonly Mock<IMapper> mockMapper;

        public RamMetricsControllerUnitTests()
        {
            mockRepository = new Mock<IRamMetricsRepository>();
            mockLogger = new Mock<ILogger<RamMetricsController>>();
            mockMapper = new Mock<IMapper>();
            controller = new RamMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
        }


        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.Create(It.IsAny<RamMetric>())).Verifiable();

            // ��������� �������� �� �����������
            IActionResult result = controller.Create(new RamMetricCreateRequest { Time = TimeSpan.FromSeconds(1).ToString(), Value = 50 });
            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.Create(It.IsAny<RamMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Create_ShouldCall_GetAvailable_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Returns(new List<RamMetric>());

            // ��������� �������� �� �����������
            var result = controller.GetAvailable(new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 50));


            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.Once());
        }
        [Fact]
        public void Create_ShouldCall_GetAll_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.GetAll()).Returns(new List<RamMetric>());

            // ��������� �������� �� �����������
            var result = controller.GetAll();


            // ��������� �������� �� ��, ��� ���� ������� ����������.
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.GetAll());
        }
    }
}