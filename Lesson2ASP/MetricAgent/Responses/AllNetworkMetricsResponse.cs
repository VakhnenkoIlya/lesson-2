using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent.Responses
{
    public class AllNetworkMetricsResponse
    { 
        public List<NetworkMetricDto> Metrics { get; set; }
    }
}
