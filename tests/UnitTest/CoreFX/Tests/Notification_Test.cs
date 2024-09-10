using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CoreFX.Abstractions.App_Start;
using CoreFX.Abstractions.Consts;
using CoreFX.Abstractions.Notification.Models;
using CoreFX.Notification.Services;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using TestAbstractions.App_Start;
using Xunit;

namespace UnitTest.CoreFX.Tests
{
    public class Notification_Test : UnitTestBase
    {
        //[Fact]
        [Fact(Skip = "Require COREFX_SMTP_PWD in the environment variable")]
        public async Task Email_Test()
        {
            var smtpPwd = Environment.GetEnvironmentVariable(EnvConst.SMTP_PWD);
            Assert.NotNull(smtpPwd);

            var sw = new Stopwatch();
            var settings = new EmailConfiguration
            {
                From = "admin@kevinw.net",
                To = "admin+xunit@kevinw.net",
                SmtpConfig = new SmtpCofiguration
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    Username = "admin@kevinw.net",
                }
            };

            var svc = new EmailService(NullLogger<EmailService>.Instance, Options.Create(settings));
            var res = await svc.SendAsync(
                subject: $"UnitTest event: {SdkRuntime.ApiName}",
                html: $"Awesome!<br><hr>UTC: {DateTime.UtcNow.ToString("s")}",
                from: settings.From,
                to: settings.To
            );
            sw.Stop();

            Assert.True(res?.IsSuccess);
            Assert.True(sw.Elapsed < TimeSpan.FromSeconds(5));
        }
    }
}
