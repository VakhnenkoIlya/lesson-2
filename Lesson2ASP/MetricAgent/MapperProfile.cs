using MetricAgent.Responses;
using System;

using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent
{

    
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                // добавлять сопоставления в таком стиле нужно для всех объектов 
                CreateMap<CpuMetric, CpuMetricDto>();
                CreateMap<DotNetMetric, DotNetMetricDto>();
                CreateMap<HddMetric, HddMetricDto>();
                CreateMap<NetworkMetric, NetworkMetricDto>();
                CreateMap<HddMetric, HddMetricDto>();
            }
        }
    
}
