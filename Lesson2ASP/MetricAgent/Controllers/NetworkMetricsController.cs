using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using MetricAgent.Responses;
using MetricAgent.Requests;
using MetricsAgent.DAL;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private INetworkMetricsRepository repository;
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
            this.repository = repository;
        }
        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetwork(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"GetNetwork с параметрами fromTime{fromTime} toTime{toTime}");
            var metrics = repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllNetworkMetricsResponse
            {
                Metrics = new List<NetworkMetricDto>()
            };

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new NetworkMetricDto { Time = metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
        [HttpPost("create")]

        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            repository.Create(new NetworkMetric
            {
                Time = TimeSpan.Parse(request.Time),
                Value = request.Value
            });

            return Ok();
        }
    }
}
