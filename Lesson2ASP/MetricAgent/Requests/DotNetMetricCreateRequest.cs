using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent.Requests
{
    public class DotNetMetricCreateRequest
    { 
        public string Time { get; set; }
        public int Value { get; set; }
    }
}
