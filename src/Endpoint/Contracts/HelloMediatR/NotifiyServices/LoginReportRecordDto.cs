using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoreFX.Abstractions.Notification.Interfaces;

namespace Hello.MediatR.Domain.Contract.NotifiyServices
{
    public class LoginReportRecordDto : IReportRecordDto
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
