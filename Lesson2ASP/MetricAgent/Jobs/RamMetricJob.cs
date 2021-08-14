using MetricsAgent.DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent.Jobs
{

    public class RamMetricJob : IJob
    {
        private readonly IRamMetricsRepository _repository;
        // счетчик для метрики CPU
        private PerformanceCounter _ramCounter;

        public RamMetricJob(IRamMetricsRepository repository)
        {
            _repository = repository;
#pragma warning disable CA1416 // Проверка совместимости платформы
            _ramCounter = new PerformanceCounter("Memory", "Available MBytes");
#pragma warning restore CA1416 // Проверка совместимости платформы
        }
        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
#pragma warning disable CA1416 // Проверка совместимости платформы
            var ramUsageInPercents = Convert.ToInt32(_ramCounter.NextValue());
#pragma warning restore CA1416 // Проверка совместимости платформы

            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new RamMetric { Time = time, Value = ramUsageInPercents });
            // теперь можно записать что-то при помощи репозитория
            return Task.CompletedTask;
        }
    }
}
