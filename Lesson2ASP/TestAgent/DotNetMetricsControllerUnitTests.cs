
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
using AutoMapper;

namespace TestAgent
{
    public class DotNetMetricsControllerUnitTests
    {
        private readonly DotNetMetricsController controller;
        private readonly Mock<IDotNetMetricsRepository> mockRepository;
        private readonly Mock<ILogger<DotNetMetricsController>> mockLogger;
        private readonly Mock<IMapper> mockMapper;


        public DotNetMetricsControllerUnitTests()
        {
            mockRepository = new Mock<IDotNetMetricsRepository>();
            mockLogger = new Mock<ILogger<DotNetMetricsController>>();
            mockMapper = new Mock<IMapper>();
            controller = new DotNetMetricsController(mockLogger.Object, mockRepository.Object, mockMapper.Object);
        }

        [Fact]
        public void Create_ShouldCall_GetEror_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� ��������  ������
            mockRepository.Setup(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Returns(new List<DotNetMetric>()); 

            // ��������� �������� �� �����������
            var result = controller.GetEror(new TimeSpan(0, 0, 1), new TimeSpan(0, 0, 50));


            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.GetByTimePeriod(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()), Times.Once());
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.Create(It.IsAny<DotNetMetric>())).Verifiable();

            // ��������� �������� �� �����������
            var result = controller.Create(new DotNetMetricCreateRequest { Time = TimeSpan.FromSeconds(1).ToString(), Value = 50 }); 


            // ��������� �������� �� ��, ��� ���� ������� ����������
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.Create(It.IsAny<DotNetMetric>()), Times.AtMostOnce());
        }
        [Fact]
        public void Create_ShouldCall_GetAll_From_Repository()
        {
            // ������������� �������� ��������
            // � �������� ����������� ��� � ����������� �������� CpuMetric ������
            mockRepository.Setup(repository => repository.GetAll()).Returns(new List<DotNetMetric>()); 

            // ��������� �������� �� �����������
            var result = controller.GetAll();


            // ��������� �������� �� ��, ��� ���� ������� ����������.
            // ������������� �������� ����� Create ����������� � ������ ����� ������� � ���������
            mockRepository.Verify(repository => repository.GetAll());
        }
    }
}
