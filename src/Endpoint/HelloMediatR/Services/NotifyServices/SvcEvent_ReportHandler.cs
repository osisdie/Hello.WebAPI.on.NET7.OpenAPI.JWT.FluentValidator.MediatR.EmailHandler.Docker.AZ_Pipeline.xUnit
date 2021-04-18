using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreFX.Abstractions.Utils;
using CoreFX.Hosting.Extensions;
using CoreFX.Notification.Models;
using Hello.MediatR.Domain.Contract.NotifiyServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hello.MediatR.Endpoint.Services.NotifyServices
{
    public class SvcEvent_ReportHandler : INotificationHandler<SvcEvent_MetadataDto>
    {
        protected readonly bool _enabled = true;
        protected readonly bool _includeDetailenabled = false;
        protected readonly ILogger _logger;
        protected readonly IConfiguration _config;
        protected readonly ISvcSchedule_ReportService<LoginReportRecordDto> _svcReport;
        protected readonly JsonSerializerSettings _jsonSetting = SerializerUtil.DefaultNotifyJsonSetting;

        public SvcEvent_ReportHandler(ILogger<SvcEvent_ReportHandler> logger, IConfiguration config, ISvcSchedule_ReportService<LoginReportRecordDto> svcReport = null)
        {
            _logger = logger;
            _config = config;
            _svcReport = svcReport;
        }

        public async Task Handle(SvcEvent_MetadataDto eventDto, CancellationToken cancellationToken)
        {
            if (!_enabled || _svcReport == null)
            {
                return;
            }

            if (eventDto?.ResponseDto == null || eventDto.ResponseDto.IsSuccess == true)
            {
                return;
            }

            try
            {
                var rec = new LoginReportRecordDto
                {
                    From = eventDto.From,
                    Category = eventDto.Category,
                    IsSuccess = eventDto.ResponseDto?.IsSuccess == true,
                };

                var meta = new Dictionary<string, string>{
                    { "From", $"{eventDto.From}" },
                    { "Category", $"{eventDto.Category}" },
                    { "IsSuccess", eventDto.IsSuccess.ToString() },
                    { "UTC", DateTime.UtcNow.ToString("s") },
                    { "Request", eventDto.RequestDto.ToJson(_jsonSetting) },
                    { "Response", eventDto.ResponseDto.ToJson(_jsonSetting) },
                };

                if (eventDto.Context is HttpContext c)
                {
                    meta["Headers"] = c.Request.Headers.ToJson(_jsonSetting);

                    var tokenDto = c.GetTokenDto();
                    if (!string.IsNullOrEmpty(tokenDto?.UserName))
                    {
                        meta["User"] = tokenDto.UserName;
                        rec.Who = tokenDto.UserName;
                    }
                }

                if (_includeDetailenabled)
                {
                    var body = "<ul>" + string.Join("", meta.Select(x => $"<li>{x.Key} : {x.Value}</li>")) + "</ul>";
                    rec.Detail = body;
                }

                rec.Who ??= eventDto.User;
                _svcReport.AddRecord(rec);
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
            }
        }
    }
}
