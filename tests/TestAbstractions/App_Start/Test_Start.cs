using System.IO;
using CoreFX.Abstractions.App_Start;
using CoreFX.Abstractions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UnitTest.Common.Extensions;

namespace TestAbstractions.App_Start
{
    public static class Test_Start
    {
        public static void Configure(this UnitTestBase app)
        {
            LaunchSettings_Extension.SetEnvironmentVariables();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory());

            var configPathList = app.GetConfigPathList();
            foreach (var config in configPathList)
            {
                builder.AddJsonFile(config.Key, optional: config.Value);
            }

            var configuration = builder.Build();
            SdkRuntime.Configuration = configuration;

            var logPath = app.GetLogPath();
            if (File.Exists(logPath))
            {
                var loggerFactory = new LoggerFactory();
                loggerFactory.AddLog4Net(logPath);
                LogMgr.LoggerFactory = loggerFactory;
            }

            SvcContext.InitialSDK();
            app.AfterConfigured(SdkRuntime.Configuration, LogMgr.LoggerFactory);
        }
    }
}
