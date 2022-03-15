using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibrary.Logger
{
    public class WebSericeLogLogger : ILogger
    {
        protected readonly WebSericeLogProvider ServiceProvider;

        public WebSericeLogLogger(WebSericeLogProvider service)
        {
            this.ServiceProvider = service;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string LogData = DateTime.Now.ToString();
            string dataException = exception != null ? exception.Message : state.ToString();
            Log log = new Log()
            {
                Data = $"{LogData} {dataException}",
                Level = logLevel.ToString()
            };
            string dataToSend = JsonSerializer.Serialize<Log>(log);
            StringContent content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
            Task.Run(async delegate
            {
                HttpResponseMessage response = await ServiceProvider.Client.PostAsync("log/save", content);
            });
        }
    }
}
