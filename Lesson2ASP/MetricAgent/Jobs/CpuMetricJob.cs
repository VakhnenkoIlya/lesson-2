using System;
using MetricAgent.DAL;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using MetricsAgent.DAL;
using System.Diagnostics;

namespace MetricAgent.Jobs
{
    public class CpuMetricJob : IJob
    {
        private readonly ICpuMetricsRepository _repository;
        // счетчик для метрики CPU
        private PerformanceCounter _cpuCounter;
       
        public CpuMetricJob(ICpuMetricsRepository repository)
        {
            _repository = repository;
#pragma warning disable CA1416 // Проверка совместимости платформы
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
#pragma warning restore CA1416 // Проверка совместимости платформы
        }
        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
            var cpuUsageInPercents = Convert.ToInt32(_cpuCounter.NextValue());

            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new CpuMetric { Time = time, Value = cpuUsageInPercents });
            // теперь можно записать что-то при помощи репозитория
            return Task.CompletedTask;
        }
    }

}
