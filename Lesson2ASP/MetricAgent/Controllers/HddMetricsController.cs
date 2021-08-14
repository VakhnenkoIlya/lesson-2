﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MetricAgent.Requests;
using MetricAgent.Responses;
using MetricsAgent.DAL;
using AutoMapper;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private IHddMetricsRepository repository;
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IMapper mapper;
        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetHdd(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"GetHdd с параметрами fromTime {fromTime} toTime {toTime}");
           
            var metrics = repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllHddMetricsResponse
            {
                Metrics = new List<HddMetricDto>()
            };
            
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<HddMetricDto>(metric));
            }

            return Ok(response);
        }
        [HttpPost("create")]

        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation($"Create с параметрами Time = {TimeSpan.Parse(request.Time)}  Value = {request.Value}");

            repository.Create(mapper.Map<HddMetric>(request));


            return Ok();
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation($"GetAll");

            var metrics = repository.GetAll();

            var response = new AllHddMetricsResponse()
            {
                Metrics = new List<HddMetricDto>()
            };


            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<HddMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
