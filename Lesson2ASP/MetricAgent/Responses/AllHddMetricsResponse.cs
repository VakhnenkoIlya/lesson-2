using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent.Responses
{
    public class AllHddMetricsResponse
    { 
        public List<HddMetricDto> Metrics { get; set; }
    }
}
