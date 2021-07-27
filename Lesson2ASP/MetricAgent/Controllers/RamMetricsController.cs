using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MetricAgent.Requests;
using MetricAgent.Responses;
using MetricsAgent.DAL;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private IRamMetricsRepository repository;
        private readonly ILogger<RamMetricsController> _logger;

        public RamMetricsController(ILogger<RamMetricsController> logger, IRamMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в RamMetricsController");
            this.repository = repository;
        }
        [HttpGet("available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetAvailable(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"GetAvailable с параметрами fromTime{fromTime} toTime{toTime}");
            var metrics = repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllRamMetricsResponse
            {
                Metrics = new List<RamMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new RamMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
        [HttpPost("create")]

        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            repository.Create(new RamMetric
            {
                Time = TimeSpan.Parse(request.Time),
                Value = request.Value
            });

            return Ok();
        }
    }
}
