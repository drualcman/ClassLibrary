using ClassLibrary.Javascript;
using ClassLibrary.Security;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClassLibrary.Extensions 
{
    public static partial class HttpClientJsonExtensions
    {
        public static async Task<HttpResponseMessage> SendAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, HttpMethod method, string requestUri, HttpContent value = null)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.SendAuthAsync(token, method, requestUri,value);
        }

        public static async Task<HttpResponseMessage> GetAuthAsync(this HttpClient httpClient, string requestUri, IJSRuntime jsRuntime)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.GetAuthAsync(token, requestUri);
        }

        public static async Task<object> GetAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.GetAuthAsync<object>(token, requestUri);
        }

        public static async Task<TValue> GetAuthAsync<TValue>(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.GetAuthAsync<TValue>(token, requestUri);
        }

        public static async Task<HttpResponseMessage> PostAuthAsync<TValue>(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, TValue value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.PostAuthAsync(token, requestUri, value);
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, FormUrlEncodedContent value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.PostAuthAsync(token, requestUri, value);
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, HttpContent value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.PostAuthAsync(token, requestUri, value);
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, MultipartFormDataContent value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.PostAuthAsync(token, requestUri, value);
        }


        #region put
        public static async Task<HttpResponseMessage> PutAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.PutAuthAsync(token, requestUri);
        }
        public static async Task<HttpResponseMessage> PutAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, HttpContent value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.PutAuthAsync(token, requestUri, value);
        }
        public static async Task<HttpResponseMessage> PutAuthAsync<TValue>(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, TValue value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.PutAuthAsync(token, requestUri, value);
        }
        #endregion


        #region delete
        public static async Task<HttpResponseMessage> DeleteAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.DeleteAuthAsync(token, requestUri);
        }
        public static async Task<TValue> DeleteAuthAsync<TValue>(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            return await httpClient.DeleteAuthAsync<TValue>(token, requestUri);
        }
        #endregion
    }
}
