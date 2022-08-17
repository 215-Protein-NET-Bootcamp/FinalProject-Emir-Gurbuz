using Core.CrossCuttingConcerns.Logging.Serilog.ConfigurationModels;
using Core.Exceptions.Log;
using Core.Utilities.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Formatting.Json;

namespace Core.CrossCuttingConcerns.Logging.Serilog.Loggers
{
    public class FileLogger : LoggerServiceBase
    {
        public FileLogger()
        {
            IConfiguration configuration = ServiceTool.ServiceProvider.GetService<IConfiguration>();

            FileLogConfiguration logConfig = configuration.GetSection("SeriLogConfigurations:FileLogConfiguration")
                .Get<FileLogConfiguration>() ??
                throw new SerilogNotFoundFolderPathException();

            var logFilePath = string.Format("{0}{1}", Directory.GetCurrentDirectory() + logConfig.FolderPath, "log.txt");
            Logger = new LoggerConfiguration()
                .WriteTo.File(
                logFilePath,
                rollingInterval: RollingInterval.Hour,
                retainedFileCountLimit: null,
                fileSizeLimitBytes: 500000,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}\n"
                ).CreateLogger();
        }
    }
}
