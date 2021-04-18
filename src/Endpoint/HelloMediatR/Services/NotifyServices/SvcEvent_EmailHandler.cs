using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoreFX.Abstractions.App_Start;
using CoreFX.Abstractions.Utils;
using CoreFX.Hosting.Extensions;
using CoreFX.Notification.Interfaces;
using CoreFX.Notification.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Hello.MediatR.Endpoint.Services.NotifyServices
{
    public class SvcEvent_EmailHandler : INotificationHandler<SvcEvent_MetadataDto>
    {
        protected readonly bool _enabled = false;
        protected readonly ILogger _logger;
        protected readonly IConfiguration _config;
        protected readonly IEmailService _svcEmail;
        protected readonly JsonSerializerSettings _jsonSetting = SerializerUtil.DefaultNotifyJsonSetting;

        public SvcEvent_EmailHandler(ILogger<SvcEvent_EmailHandler> logger,
            IConfiguration config, 
            IEmailService svcEmail = null)
        {
            _logger = logger;
            _config = config;
            _svcEmail = svcEmail;
        }

        public async Task Handle(SvcEvent_MetadataDto eventDto, CancellationToken cancellationToken)
        {
            if (!_enabled || _svcEmail == null)
            {
                return;
            }

            if (eventDto?.ResponseDto == null || eventDto.ResponseDto.IsSuccess == true)
            {
                return;
            }

            try
            {
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
                    }
                }

                var subject = eventDto.ResponseDto.IsSuccess ? "success" : "error" + $" event: {SdkRuntime.ApiName} - {eventDto.From}";
                var body = "<ul>" + string.Join("", meta.Select(x => $"<li>{x.Key} : {x.Value}</li>")) + "</ul>";
                _ = Task.Run(async () =>
                {
                    await _svcEmail.SendAsync(subject: subject, html: body);
                });

                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
            }
        }
    }
}
