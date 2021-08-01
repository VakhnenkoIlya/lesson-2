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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private IHddMetricsRepository repository;
        private readonly ILogger<HddMetricsController> _logger;

        public HddMetricsController(ILogger<HddMetricsController> logger, IHddMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в HddMetricsController");
            this.repository = repository;
        }

        

        [HttpGet("left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetHdd(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"GetHdd с параметрами fromTime{fromTime} toTime{toTime}");
           
            var metrics = repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllHddMetricsResponse
            {
                Metrics = new List<HddMetricDto>()
            };
            if (metrics!=null)
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new HddMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
        [HttpPost("create")]

        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            repository.Create(new HddMetric
            {
                Time = TimeSpan.Parse(request.Time),
                Value = request.Value
            });

            return Ok();
        }
    }
}
