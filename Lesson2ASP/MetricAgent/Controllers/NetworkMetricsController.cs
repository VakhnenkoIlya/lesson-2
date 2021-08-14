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
using AutoMapper;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private INetworkMetricsRepository repository;
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly IMapper mapper;
        public NetworkMetricsController(ILogger<NetworkMetricsController> logger, INetworkMetricsRepository repository, IMapper mapper)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в NetworkMetricsController");
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetwork(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"GetNetwork с параметрами fromTime {fromTime} toTime {toTime}");
            var metrics = repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllNetworkMetricsResponse
            {
                Metrics = new List<NetworkMetricDto>()
            };
        
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }
        [HttpPost("create")]

        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation($"Create с параметрами Time = {TimeSpan.Parse(request.Time)}  Value = {request.Value}");

            repository.Create(mapper.Map<NetworkMetric>(request));

            return Ok();
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            _logger.LogInformation($"GetAll");

            var metrics = repository.GetAll();

            var response = new AllNetworkMetricsResponse()
            {
                Metrics = new List<NetworkMetricDto>()
            };


            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<NetworkMetricDto>(metric));
            }

            return Ok(response);
        }
    }
}
