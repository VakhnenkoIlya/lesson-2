using MetricsAgent.DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent.Jobs
{
    public class DotNetMetricJob : IJob
    {
        private readonly IDotNetMetricsRepository _repository;
        // счетчик для метрики CPU
        private PerformanceCounter _dotNetCounter;

        public DotNetMetricJob(IDotNetMetricsRepository repository)
        {
            _repository = repository;
#pragma warning disable CA1416 // Проверка совместимости платформы
            _dotNetCounter = new PerformanceCounter(".NET CLR Memory", "# Bytes in all Heaps", "_Global_");
#pragma warning restore CA1416 // Проверка совместимости платформы
        }
        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости 
#pragma warning disable CA1416 // Проверка совместимости платформы
            var dotNetUsageInPercents = Convert.ToInt32(_dotNetCounter.NextValue());
#pragma warning restore CA1416 // Проверка совместимости платформы

            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new DotNetMetric {Time = time, Value = dotNetUsageInPercents});
            // теперь можно записать что-то при помощи репозитория
            return Task.CompletedTask;
        }
    }
}
