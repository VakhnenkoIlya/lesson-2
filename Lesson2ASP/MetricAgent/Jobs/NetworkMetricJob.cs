using MetricsAgent.DAL;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MetricAgent.Jobs
{
    public class NetworkMetricJob : IJob
    {
        private readonly INetworkMetricsRepository _repository;
        // счетчик для метрики CPU
        private PerformanceCounter _networkCounter;

        public NetworkMetricJob(INetworkMetricsRepository repository)
        {
            _repository = repository;
            PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
#pragma warning disable CA1416 // Проверка совместимости платформы
            _networkCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", performanceCounterCategory.GetInstanceNames()[0]);
#pragma warning restore CA1416 // Проверка совместимости платформы
        }
        public Task Execute(IJobExecutionContext context)
        {
            // получаем значение занятости CPU
#pragma warning disable CA1416 // Проверка совместимости платформы
            var networkUsageInPercents = Convert.ToInt32(_networkCounter.NextValue());
#pragma warning restore CA1416 // Проверка совместимости платформы

            // узнаем когда мы сняли значение метрики.
            var time = TimeSpan.FromSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds());

            // теперь можно записать что-то при помощи репозитория

            _repository.Create(new NetworkMetric { Time = time, Value = networkUsageInPercents });
            // теперь можно записать что-то при помощи репозитория
            return Task.CompletedTask;
        }
    }
}
