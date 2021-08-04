using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Data.SQLite;

using MetricsAgent.DAL;
using MetricAgent.Requests;
using MetricAgent.Responses;
using Moq;

namespace MetricAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {

        [HttpGet("sql-test")]
        public IActionResult TryToSqlLite()
        {
            string cs = "Data Source=:memory:";
            string stm = "SELECT SQLITE_VERSION()";


            using (var con = new SQLiteConnection(cs))
            {
                con.Open();

                using var cmd = new SQLiteCommand(stm, con);
                string version = cmd.ExecuteScalar().ToString();

                return Ok(version);
            }
        }

        private ICpuMetricsRepository repository;
        private readonly ILogger<CpuMetricsController> _logger;

        
        public CpuMetricsController(ILogger<CpuMetricsController> logger, ICpuMetricsRepository repository)


        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
            this.repository = repository;
            
        }


        [HttpGet("from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetrics(TimeSpan fromTime, TimeSpan toTime)
        {
            _logger.LogInformation($"GetMetrics с параметрами fromTime{fromTime} toTime{toTime}");
            
            var metrics = repository.GetByTimePeriod(fromTime, toTime);

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            if (metrics != null)

                foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Time = (TimeSpan)metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
  
        [HttpPost("create")]
        public IActionResult Create([FromBody] CpuMetricCreateRequest request)
        {
            repository.Create(new CpuMetric
            {
                Time = request.Time,
                Value = request.Value
                
            });

            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = repository.GetAll();

            var response = new AllCpuMetricsResponse()
            {
                Metrics = new List<CpuMetricDto>()
            };
            if (metrics != null)

            foreach (var metric in metrics)
            {
                response.Metrics.Add(new CpuMetricDto { Time = (TimeSpan)metric.Time, Value = metric.Value, Id = metric.Id });
            }

            return Ok(response);
        }
    }
}
        

    



