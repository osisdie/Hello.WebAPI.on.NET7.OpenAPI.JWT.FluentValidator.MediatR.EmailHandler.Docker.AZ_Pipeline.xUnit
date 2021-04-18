using System;
using System.Collections.Generic;
using System.IO;
using CoreFX.Abstractions.App_Start;
using CoreFX.Abstractions.Consts;
using CoreFX.Abstractions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace TestAbstractions.App_Start
{
    public abstract class UnitTestBase
    {
        protected readonly ILogger _logger;

        protected UnitTestBase(ILogger logger = null)
        {
            Initialization();

            _logger = logger ?? LogMgr.CreateLogger(GetType());
        }

        private void Initialization()
        {
            if (_isInitialized)
            {
                return;
            }

            lock (_lock)
            {
                if (!_isInitialized)
                {
                    this.Configure();
                }
            }
        }

        public virtual Dictionary<string, bool> GetConfigPathList() => new Dictionary<string, bool>
        {
            { Path.Combine(SvcConst.DefaultConfigFolder, SvcConst.DefaultAppSettingsFile), false },
        };

        public virtual string GetLogPath() => Path.Combine(SvcConst.DefaultConfigFolder, SvcConst.DefaultLog4netConfigFile);
        public virtual void AfterConfigured(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            //var env = Environment.GetEnvironmentVariable(EnvConst.AspNetCoreEnvironment);
            Console.WriteLine($"{EnvConst.AspNetCoreEnvironment}={SdkRuntime.SdkEnv}");
        }

        protected static bool _isInitialized = false;
        private static readonly object _lock = new object();
    }
}
