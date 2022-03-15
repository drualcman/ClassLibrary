using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClassLibrary.Extensions
{
    public static partial class HttpClientJsonExtensions
    {
        private static async Task<string> GetTocken(IAccessTokenProvider accessTokenProvider)
        {
            AccessTokenResult accessTokenResult = await accessTokenProvider.RequestAccessToken();
            string tokenResult;
            if (accessTokenResult.TryGetToken(out AccessToken token))
            {
                tokenResult = token.Value;
            }
            else
            {
                tokenResult = string.Empty;
            }
            return tokenResult;
        }

        public static async Task<HttpResponseMessage> SendAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, HttpMethod method, string requestUri, HttpContent value = null)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.SendAuthAsync(token, method, requestUri, value);
        }

        public static async Task<HttpResponseMessage> GetAuthAsync(this HttpClient httpClient, string requestUri, IAccessTokenProvider accessTokenProvider)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.GetAuthAsync(token, requestUri);
        }

        public static async Task<object> GetAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.GetAuthAsync<object>(token, requestUri);
        }

        public static async Task<TValue> GetAuthAsync<TValue>(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.GetAuthAsync<TValue>(token, requestUri);
        }

        public static async Task<HttpResponseMessage> PostAuthAsync<TValue>(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, TValue value)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.PostAuthAsync(token, requestUri, value);
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, FormUrlEncodedContent value)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.PostAuthAsync(token, requestUri, value);
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, HttpContent value)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.PostAuthAsync(token, requestUri, value);
        }

        public static async Task<HttpResponseMessage> PostAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, MultipartFormDataContent value)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.PostAuthAsync(token, requestUri, value);
        }


        #region put
        public static async Task<HttpResponseMessage> PutAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.PutAuthAsync(token, requestUri);
        }
        public static async Task<HttpResponseMessage> PutAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, HttpContent value)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.PutAuthAsync(token, requestUri, value);
        }
        public static async Task<HttpResponseMessage> PutAuthAsync<TValue>(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri, TValue value)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.PutAuthAsync(token, requestUri, value);
        }
        #endregion


        #region delete
        public static async Task<HttpResponseMessage> DeleteAuthAsync(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.DeleteAuthAsync(token, requestUri);
        }
        public static async Task<TValue> DeleteAuthAsync<TValue>(this HttpClient httpClient, IAccessTokenProvider accessTokenProvider, string requestUri)
        {
            string token = await GetTocken(accessTokenProvider);
            return await httpClient.DeleteAuthAsync<TValue>(token, requestUri);
        }
        #endregion
    }
}
