using System.IO;
using CoreFX.Abstractions.App_Start;
using CoreFX.Abstractions.Consts;
using CoreFX.Abstractions.Extensions;
using Hello.MediatR.Domain.Contract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Hello.MediatR.Endpoint
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SvcContext.InitialSDK();
            SdkRuntime.ApiName ??= HelloMediatRSvcConfig.ServiceName;
            SdkRuntime.DeploymentName ??= HelloMediatRSvcConfig.DeployName;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.Sources.Clear();

                    var env = hostingContext.HostingEnvironment;
                    config.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile(SvcConst.DefaultAppSettingsFile, optional: false, reloadOnChange: true)
                        .AddJsonFile(SvcConst.DefaultAppSettingsFile.AddingBeforeExtension(env.EnvironmentName), optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables(prefix: EnvConst.DOTNET_prefix)
                        .AddEnvironmentVariables(prefix: EnvConst.ASPNETCORE_prefix)
                        .AddEnvironmentVariables(prefix: EnvConst.AWS_prefix);

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
