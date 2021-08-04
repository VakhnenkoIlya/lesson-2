using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SQLite;

namespace MetricAgent
{
    public class DotNetMetric
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public TimeSpan Time { get; set; }
    }

}











