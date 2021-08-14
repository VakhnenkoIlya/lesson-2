using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using MetricsAgent.DAL;
using MetricAgent.Requests;
using MetricAgent.Responses;
using AutoMapper;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private ICpuMetricsRepository repository;
        private readonly ILogger<CpuMetricsController> _logger;
        private readonly IMapper mapper;

        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository, IMapper mapper)


        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            this.repository = repository;
            this.mapper = mapper;
            
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"GetMetrics с параметрами fromTime {fromTime} toTime {toTime}");
            
            var metrics = repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
       

                foreach (var metric in metrics)
                {
                    response.Metrics.Add(mapper.Map<CpuMetricDto>(metric));
                }

            return Ok(response);
        }
  
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            _logger.LogInformation($"Create с параметрами Time { request.Time} Value  {request.Value}");
            var response = new AllCpuMetricsResponse();

            repository.Create(mapper.Map<CpuMetric>(request));
             
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation($"GetAll");

            var metrics = repository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
           

            foreach (var metric in metrics)
            {
                    response.Metrics.Add(mapper.Map<CpuMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
        

    



