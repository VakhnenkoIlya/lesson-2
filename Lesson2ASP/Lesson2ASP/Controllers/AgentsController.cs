using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lesson2ASP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(ILogger<AgentsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в AgentsMetricsController");
        }

      

        private readonly static List<AgentInfo> _OfAgent = new();
        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _OfAgent.Add(agentInfo);
            _logger.LogInformation($"RegisterAgent с параметрами agentId {agentInfo} agentAdress {agentInfo}");
            return Ok();
        }

        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"EnableAgentById с параметрами agentId {agentId}");
            return Ok();
        }
        [HttpGet]
        public IActionResult ShowAgent()
        {
            _logger.LogInformation($"ShowAgent");
            return Ok(_OfAgent);
                
        }

        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"DisableAgentById с параметрами agentId {agentId}");
            return Ok();
        }
    }
  
}
