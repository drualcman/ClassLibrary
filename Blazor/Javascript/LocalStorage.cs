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
    public static class LocalStorage
    {
        /// <summary>
        /// Recovery data from LocalStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <returns></returns>
        public static async ValueTask<string> LocalStorageGetAsync(this IJSRuntime jsRuntume, string key) 
            => await jsRuntume.InvokeAsync<string>("MyLocalStorage.Get", key);

        /// <summary>
        /// Recovery data from LocalStorage
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="jsRuntume"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async ValueTask<TModel> LocalStorageGetAsync<TModel>(this IJSRuntime jsRuntume, string key) 
        {
            string data = await LocalStorageGetAsync(jsRuntume, key);
            return JsonSerializer.Deserialize<TModel>(data);            
        }

        /// <summary>
        /// Save data to LocalStorage
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="jsRuntume"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void LocalStorageSetAsync<TModel>(this IJSRuntime jsRuntume, string key, TModel value)
        {
            string jsonData = JsonSerializer.Serialize(value);
            LocalStorageSetAsync(jsRuntume, key, jsonData);
        }

        /// <summary>
        /// Save data to LocalStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <param name="value">object with the data to store</param>
        /// <returns></returns>
        public static async void LocalStorageSetAsync(this IJSRuntime jsRuntume, string key, string value)
            => await jsRuntume.InvokeVoidAsync("MyLocalStorage.Set", key, value);

        /// <summary>
        /// Remove data to LocalStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <returns></returns>
        public static async void LocalStorageDelAsync(this IJSRuntime jsRuntume, string key)
            => await jsRuntume.InvokeVoidAsync("MyLocalStorage.Del", key);
    }
}
