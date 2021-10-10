using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;

namespace ClassLibrary.Service
{
    /// <summary>
    /// Service interface for our DefaultServices service
    /// </summary>
    public interface IDefaultServices
    {
        public HttpClient Client { get; }
        public NavigationManager Navigation { get; }
        public IJSRuntime JsRuntime { get; }
        public IAccessTokenProvider AccessTokenProvider { get; }
    }    
}
