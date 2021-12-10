using ClassLibrary.Logger;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System.Net.Http;

namespace ClassLibrary.Services
{
    public class DefaultServices : IDefaultServices
    {
        public DefaultServices(HttpClient Client,
            NavigationManager nav,
            IJSRuntime js)
        {
            this.Client = Client;
            Navigation = nav;
            JsRuntime = js;
        }

        public DefaultServices(HttpClient Client,
            NavigationManager nav,
            IJSRuntime js,
            IAccessTokenProvider accessTokenProvider)
        {
            this.Client = Client;
            Navigation = nav;
            JsRuntime = js;
            AccessTokenProvider = accessTokenProvider;
        }

        public HttpClient Client { get; }
        public NavigationManager Navigation { get; }
        public IJSRuntime JsRuntime { get; }
        public IAccessTokenProvider AccessTokenProvider { get; }
    }
}
