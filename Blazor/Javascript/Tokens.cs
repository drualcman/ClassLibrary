using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace ClassLibrary.Javascript
{
    public static class Tokens
    {
        #region get
        /// <summary>
        /// Get the access token about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task<string> GetUserTokenAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.LocalStorageGetAsync("token");
        }

        /// <summary>
        /// Get the claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task<string> GetUserDataAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.LocalStorageGetAsync("blazor");
        }
        #endregion

        #region Set
        /// <summary>
        /// Set the access token the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static Task SetUserTokenAsync(this IJSRuntime jsRuntime, string token)
        {
            jsRuntime.LocalStorageSetAsync("token", token);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Set the claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static Task SetUserDataAsync(this IJSRuntime jsRuntime, string data)
        {
            jsRuntime.LocalStorageSetAsync("blazor", data);
            return Task.CompletedTask;
        }


        /// <summary>
        /// Set the access token and claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static Task SetAllDataAsync(this IJSRuntime jsRuntime, string data, string token)
        {
            jsRuntime.LocalStorageSetAsync("blazor", data);
            jsRuntime.LocalStorageSetAsync("token", token);
            return Task.CompletedTask;
        }


        /// <summary>
        /// Set the access token and claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task<bool> IsTokenExpiredAsync(this IJSRuntime jsRuntime)
        {
            string token = await jsRuntime.LocalStorageGetAsync("token");
            bool result;
            if (string.IsNullOrEmpty(token)) result = true;
            else
            {
                string payload = Cipher.Hash.Base64.Base64UrlDecode(token.Split('.')[1]);
                JsonElement jsonElement = JsonSerializer.Deserialize<JsonElement>(payload);
                JsonElement exp = jsonElement.GetProperty("exp");
                DateTimeOffset date = DateTimeOffset.FromUnixTimeSeconds(exp.GetInt64());
                result = date < DateTime.Now;
            }
            return result;
        }
        #endregion


        #region delete
        /// <summary>
        /// Delete the access token the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static Task DelUserTokenAsync(this IJSRuntime jsRuntime)
        { 
            jsRuntime.LocalStorageDelAsync("token");
            return Task.CompletedTask;
        }


        /// <summary>
        /// Delete claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static Task DelUserDataAsync(this IJSRuntime jsRuntime)
        {
            jsRuntime.LocalStorageDelAsync("blazor");
            return Task.CompletedTask;
        }

        /// <summary>
        /// Delete claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static Task DelAllDataAsync(this IJSRuntime jsRuntime)
        {
            jsRuntime.LocalStorageDelAsync("blazor");
            jsRuntime.LocalStorageDelAsync("token");
            return Task.CompletedTask;
        }

        #endregion
    }
}
