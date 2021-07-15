using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ClassLibrary.Javascript
{
    /// <summary>
    /// Interop with JAVASCRIPT to manage LocalStorage
    /// </summary>
    public static class SessionStorage
    {
        /// <summary>
        /// Recovery data from SessionStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <returns></returns>
        public static async ValueTask<string> SessionStorageGetAsync(this IJSRuntime jsRuntume, string key) => 
            await jsRuntume.InvokeAsync<string>("MySessionStorage.Get", key);

        /// <summary>
        /// Recovery data from SessionStorage
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="jsRuntume"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async ValueTask<TModel> SessionStorageGetAsync<TModel>(this IJSRuntime jsRuntume, string key)
        {
            string data = await SessionStorageGetAsync(jsRuntume, key);
            return JsonSerializer.Deserialize<TModel>(data);
        }

        /// <summary>
        /// Save data to SessionStorage
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="jsRuntume"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SessionStorageSetAsync<TModel>(this IJSRuntime jsRuntume, string key, TModel value)
        {
            string jsonData = JsonSerializer.Serialize(value);
            SessionStorageSetAsync(jsRuntume, key, jsonData);
        }

        /// <summary>
        /// Save data to SessionStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <param name="value">object with the data to store</param>
        /// <returns></returns>
        public static async void SessionStorageSetAsync(this IJSRuntime jsRuntume, string key, string value) => 
            await jsRuntume.InvokeVoidAsync("MySessionStorage.Set", key, value);

        /// <summary>
        /// Remove data to SessionStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <returns></returns>
        public static async void SessionStorageDelAsync(this IJSRuntime jsRuntume, string key)
            => await jsRuntume.InvokeVoidAsync("MySessionStorage.Del", key);
    }
}
