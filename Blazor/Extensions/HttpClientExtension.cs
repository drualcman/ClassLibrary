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
    public static class HttpClientJsonExtensions
    {
        #region with token
        public static async Task<HttpResponseMessage> SendAuthAsync(this HttpClient httpClient, string token, HttpMethod method, string requestUri)
        {
            //check is the user are authenticated
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, requestUri);
            //set the token for the authentication
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
            return response;
        }

        public static async Task<object> GetAuthAsync(this HttpClient httpClient, string token, string requestUri)
        {
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            object response = await httpClient.GetFromJsonAsync<object>(requestUri);
            return response;
        }

        public static async Task<TValue> GetAuthAsync<TValue>(this HttpClient httpClient, string token, string requestUri)
        {
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            TValue response = await httpClient.GetFromJsonAsync<TValue>(requestUri);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAuthAsync<TValue>(this HttpClient httpClient, string token, string requestUri, TValue value)
        {
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestUri, value);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, string token, string requestUri, HttpContent value)
        {
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, value);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, string token, string requestUri, FormUrlEncodedContent value)
        {
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, value);
            return response;
        }
        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, string token, string requestUri, MultipartFormDataContent value)
        {
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, value);
            return response;
        }
        #endregion

        #region with JSRuntime
        public static async Task<HttpResponseMessage> SendAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, HttpMethod method, string requestUri)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            //check is the user are authenticated
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, requestUri);
            //set the token for the authentication
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
            return response;
        }

        public static async Task<HttpResponseMessage> GetAuthAsync(this HttpClient httpClient, string requestUri, IJSRuntime jsRuntime)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync(requestUri);
            return response;
        }

        public static async Task<object> GetAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            object response = await httpClient.GetFromJsonAsync<object>(requestUri);
            return response;
        }

        public static async Task<TValue> GetAuthAsync<TValue>(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            TValue response = await httpClient.GetFromJsonAsync<TValue>(requestUri);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAuthAsync<TValue>(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, TValue value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestUri, value);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, FormUrlEncodedContent value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, value);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, HttpContent value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, value);
            return response;
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IJSRuntime jsRuntime, string requestUri, MultipartFormDataContent value)
        {
            string token = await jsRuntime.GetUserTokenAsync();
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.PostAsync(requestUri, value);
            return response;
        }

        #endregion
    }
}
