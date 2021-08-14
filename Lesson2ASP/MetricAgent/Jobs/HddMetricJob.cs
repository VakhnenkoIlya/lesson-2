using MetricsAgent.DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent.Jobs
{
    public class HddMetricJob : IJob
    {
        private readonly IHddMetricsRepository _repository;
        // счетчик для метрики CPU
        private PerformanceCounter _hddCounter;

        public HddMetricJob(IHddMetricsRepository repository)
        {
            _repository = repository;
#pragma warning disable CA1416 // Проверка совместимости платформы
            _hddCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
#pragma warning restore CA1416 // Проверка совместимости платформы
        }
        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
#pragma warning disable CA1416 // Проверка совместимости платформы
            var hddUsageInPercents = Convert.ToInt32(_hddCounter.NextValue());
#pragma warning restore CA1416 // Проверка совместимости платформы

            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new HddMetric { Time = time, Value = hddUsageInPercents });
            // теперь можно записать что-то при помощи репозитория
            return Task.CompletedTask;
        }
    }
}
