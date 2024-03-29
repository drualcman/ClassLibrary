﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ClassLibrary.Extensions
{
    public static partial class HttpClientJsonExtensions
    {
        public static async Task<HttpResponseMessage> SendAuthAsync(this HttpClient httpClient, string token, HttpMethod method, string requestUri, HttpContent value = null)
        {
            //check is the user are authenticated
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, UrlHashCheck(requestUri));
            //set the token for the authentication
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            requestMessage.Content = value;
            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
            return response;
        }

        #region GET
        public static async Task<HttpResponseMessage> GetAuthAsync(this HttpClient httpClient, string token, string requestUri)
        {
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.GetAsync(UrlHashCheck(requestUri));
            return response;
        }

        public static async Task<TValue> GetAuthAsync<TValue>(this HttpClient httpClient, string token, string requestUri)
        {
            //set the token for the authentication  
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            TValue response = await httpClient.GetFromJsonAsync<TValue>(UrlHashCheck(requestUri));
            return response;
        }
        #endregion

        #region post
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

        #region put
        public static async Task<HttpResponseMessage> PutAuthAsync(this HttpClient httpClient, string token, string requestUri) =>
            await SendAuthAsync(httpClient, token, HttpMethod.Put, requestUri);

        public static async Task<HttpResponseMessage> PutAuthAsync(this HttpClient httpClient, string token, string requestUri, HttpContent value) =>
            await SendAuthAsync(httpClient, token, HttpMethod.Put, requestUri, value);

        public static async Task<HttpResponseMessage> PutAuthAsync<TValue>(this HttpClient httpClient, string token, string requestUri, TValue value)
        {
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await httpClient.PutAsJsonAsync(requestUri, value);
        }
        #endregion

        #region delete
        public static async Task<HttpResponseMessage> DeleteAuthAsync(this HttpClient httpClient, string token, string requestUri)
        {
            //set the token for the authentication            
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await httpClient.DeleteAsync(requestUri);
            return response;
        }
        public static async Task<TValue> DeleteAuthAsync<TValue>(this HttpClient httpClient, string token, string requestUri)
        {
            //set the token for the authentication            
            HttpResponseMessage response = await DeleteAuthAsync(httpClient, token, requestUri);
            TValue result = await response.Content.ReadFromJsonAsync<TValue>();
            return result;
        }
        #endregion

        #region helpers
        private static string UrlHashCheck(string url)
        {
            string result = url.Replace("#", "%23");
            return result;
        }
        #endregion

    }
}
