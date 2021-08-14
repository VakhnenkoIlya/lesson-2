using MetricAgent.Responses;
using System;

using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricAgent.Requests;

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
                CreateMap<RamMetric, RamMetricDto>();
            CreateMap<CpuMetricCreateRequest, CpuMetric>();
            CreateMap<DotNetMetricCreateRequest, DotNetMetric>();
            CreateMap<HddMetricCreateRequest, HddMetric>();
            CreateMap<NetworkMetricCreateRequest, NetworkMetric>();
            CreateMap<RamMetricCreateRequest, RamMetric>();
            }
        }
    
}
