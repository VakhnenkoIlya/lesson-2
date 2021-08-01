using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using MetricAgent.Requests;
using MetricAgent.Responses;
using System.Threading.Tasks;
using MetricsAgent.DAL;


namespace MetricAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : ControllerBase
    {
        private IDotNetMetricsRepository repository;
        private readonly ILogger<DotNetMetricsController> _logger;

        public DotNetMetricsController(ILogger<DotNetMetricsController> logger, IDotNetMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в DotNetMetricsController");
            this.repository = repository;
        }

     

        [HttpGet("errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetEror(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"GetEror с параметрами fromTime{fromTime} toTime{toTime}");

            var metrics = repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllDotNetMetricsResponse
            {
                Metrics = new List<DotNetMetricDto>()
            };
            if (metrics!=null)
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new DotNetMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }

        [HttpPost("create")]

        public IActionResult Create([FromBody] DotNetMetricCreateRequest request)
        {
            repository.Create(new DotNetMetric
            {
                Time = TimeSpan.Parse(request.Time),
                Value = request.Value
            });

            return Ok();
        }
    }
}

