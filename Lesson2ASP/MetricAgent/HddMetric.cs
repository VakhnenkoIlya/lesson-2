using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SQLite;

namespace MetricAgent
{
    public class HddMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }

}











