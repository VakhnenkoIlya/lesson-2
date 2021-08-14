
using MetricAgent;
using MetricAgent.Controllers;

using MetricsAgent.DAL;

using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using Xunit;

using Microsoft.Extensions.Logging;
using MetricAgent.Requests;
using AutoMapper;
using System.Collections.Generic;

namespace TestAgent
{
    public class HddMetricsControllerUnitTests
    {
        private readonly HddMetricsController controller;
        private readonly Mock<IHddMetricsRepository> mockRepository;
        private readonly Mock<ILogger<HddMetricsController>> mockLogger;
        private readonly Mock<IMapper> mockMapper;


        public HddMetricsControllerUnitTests()
        {
             mockRepository = new Mock<IHddMetricsRepository>();
            mockMapper = new Mock<IMapper>();
            mockLogger = new Mock<ILogger<HddMetricsController>>();
            controller = new HddMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object); 
         }
    

        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.Create(It.IsAny<HddMetric>())).Verifiable();

            // ��������� �������� �� �����������
            IActionResult result = controller.Create(new HddMetricCreateRequest { Time = TimeSpan.FromSeconds(1).ToString(), Value = 50 });
            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.Create(It.IsAny<HddMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Create_ShouldCall_GetHdd_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Returns(new List<HddMetric>());

            // ��������� �������� �� �����������
            var result = controller.GetHdd(new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 50));


            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.Once());
        }
        [Fact]
        public void Create_ShouldCall_GetAll_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.GetAll()).Returns(new List<HddMetric>());

            // ��������� �������� �� �����������
            var result = controller.GetAll();


            // ��������� �������� �� ��, ��� ���� ������� ����������.
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.GetAll());
        }

    }
}

