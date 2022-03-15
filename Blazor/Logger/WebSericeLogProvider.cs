using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;

namespace ClassLibrary.Logger
{
    public class WebSericeLogProvider : ILoggerProvider
    {
        public readonly HttpClient Client;
        public WebSericeLogProvider(HttpClient httClient)
        {
            this.Client = httClient;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new WebSericeLogLogger(this);
        }

        #region dispose
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~WebSericeLogProvider()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
