//using ClassLibrary.Javascript;
//using ClassLibrary.Security;
//using Microsoft.JSInterop;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Net.Http.Json;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

//namespace ClassLibrary.Extensions
//{
//    public static partial class HttpClientJsonExtensions
//    {
//        private static async Task<string> GetTocken(IAccessTokenProvider accessTokenProvider)
//        {
//            AccessTokenResult accessTokenResult = await accessTokenProvider.RequestAccessToken();
//            string tokenResult;
//            if (accessTokenResult.TryGetToken(out AccessToken token))
//            {
//                tokenResult = token.Value;
//            }
//            else
//            {
//                tokenResult = string.Empty;
//            }
//            return tokenResult;
//        }

//        public static async Task<HttpResponseMessage> SendAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, HttpMethod method, string requestUri)
//        {
//            string token = await GetTocken(accessTokenProvider);
//            return await httpClient.SendAuthAsync(token, method, requestUri);
//        }

//        public static async Task<HttpResponseMessage> GetAuthAsync(this HttpClient httpClient, string requestUri, IAccessTokenProvider accessTokenProvider)
//        {
//            string token = await GetTocken(accessTokenProvider);
//            return await httpClient.GetAuthAsync(token, requestUri);
//        }

//        public static async Task<object> GetAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri)
//        {
//            string token = await GetTocken(accessTokenProvider);
//            return await httpClient.GetAuthAsync<object>(token, requestUri);
//        }

//        public static async Task<TValue> GetAuthAsync<TValue>(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri)
//        {
//            string token = await GetTocken(accessTokenProvider);
//            return await httpClient.GetAuthAsync<TValue>(token, requestUri);
//        }

//        public static async Task<HttpResponseMessage> PostAuthAsync<TValue>(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, TValue value)
//        {
//            string token = await GetTocken(accessTokenProvider);
//            return await httpClient.PostAuthAsync(token, requestUri, value);
//        }

//        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, FormUrlEncodedContent value)
//        {
//            string token = await GetTocken(accessTokenProvider);
//            return await httpClient.PostAuthAsync(token, requestUri, value);
//        }

//        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, HttpContent value)
//        {
//            string token = await GetTocken(accessTokenProvider);
//            return await httpClient.PostAuthAsync(token, requestUri, value);
//        }

//        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, MultipartFormDataContent value)
//        {
//            string token = await GetTocken(accessTokenProvider);
//            return await httpClient.PostAuthAsync(token, requestUri, value);
//        }
//    }
//}
