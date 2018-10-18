using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace Project.ConsoleApp {
    public class NLogTest {
        private readonly ILogger<NLogTest> _logger;

        public NLogTest(ILogger<NLogTest> logger) {
            _logger = logger;
        }

        public void DoAction(string name) {
            _logger.LogDebug(20, "Doing hard work! {Action}", name);
        }


        public static IServiceProvider BuildDi() {
            var services = new ServiceCollection();

            //Runner is the custom class
            services.AddTransient<NLogTest>(); 
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));
            services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace));

            var serviceProvider = services.BuildServiceProvider();

            var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            //configure NLog
            loggerFactory.AddNLog(new NLogProviderOptions
                {CaptureMessageTemplates = true, CaptureMessageProperties = true});
            LogManager.LoadConfiguration("nlog.config");

            return serviceProvider;
        }
    }
}