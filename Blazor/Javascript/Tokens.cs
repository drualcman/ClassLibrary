using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static async Task SetUserTokenAsync(this IJSRuntime jsRuntime, string token)
            => await jsRuntime.LocalStorageSetAsync("token", token);

        /// <summary>
        /// Set the claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task SetUserDataAsync(this IJSRuntime jsRuntime, string data)
            => await jsRuntime.LocalStorageSetAsync("blazor", data);


        /// <summary>
        /// Set the access token and claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task SetAllDataAsync(this IJSRuntime jsRuntime, string data, string token)
        {
            await jsRuntime.LocalStorageSetAsync("blazor", data);
            await jsRuntime.LocalStorageSetAsync("token", token);
        }
        #endregion


        #region delete
        /// <summary>
        /// Delete the access token the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task DelUserTokenAsync(this IJSRuntime jsRuntime)
            => await jsRuntime.LocalStorageDelAsync("token");

        /// <summary>
        /// Delete claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task DelUserDataAsync(this IJSRuntime jsRuntime)
            => await jsRuntime.LocalStorageDelAsync("blazor");

        /// <summary>
        /// Delete claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task DelAllDataAsync(this IJSRuntime jsRuntime)
        {
            await jsRuntime.LocalStorageDelAsync("blazor");
            await jsRuntime.LocalStorageDelAsync("token");
        }

        #endregion
    }
}
