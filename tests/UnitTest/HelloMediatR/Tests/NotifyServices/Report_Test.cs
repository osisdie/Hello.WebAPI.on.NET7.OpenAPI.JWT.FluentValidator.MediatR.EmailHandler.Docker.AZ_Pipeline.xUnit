using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoreFX.Abstractions.Notification.Interfaces;
using CoreFX.Notification.Utils;
using UnitTest.FileSigning.App_Start;
using Xunit;

namespace UnitTest.HelloMediatR.Tests.NotifyServices
{
    public class Report_Test : HelloMediatRUnitTestBase
    {
        [Fact]
        public void Table_Test()
        {
            var recs = new List<ExampleReportDecordDto>
            {
                new ExampleReportDecordDto
                {
                    Sn = 2,
                    From = "/1",
                    Category = "test",
                    IsSuccess = true,
                    Who = "tester",
                    When = DateTime.UtcNow.ToString("s")
                },
                new ExampleReportDecordDto
                {
                    Sn = 1,
                    From = "/1",
                    Category = "test",
                    IsSuccess = false,
                    Who = "tester",
                    When = DateTime.UtcNow.ToString("s")
                }
            };

            var table = recs.ToHtmlTable();
            Assert.True(!string.IsNullOrEmpty(table));
        }
    }

    public class ExampleReportDecordDto : IReportRecordDto
    {
        public int Sn { get; set; }
        public string From { get; set; }
        public string Category { get; set; }
        public string What { get; set; }
        public string Who { get; set; }
        public string When { get; set; }

        [JsonIgnore]
        public string Detail { get; set; }

        [JsonIgnore]
        public int Count { get; set; }

        [JsonIgnore]
        public bool IsSuccess { get; set; }

        [JsonIgnore]
        public DateTime _ts { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public Dictionary<string, string> ExtMap { get; set; }
    }
}
