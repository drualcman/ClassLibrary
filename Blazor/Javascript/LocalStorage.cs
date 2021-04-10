using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
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
        /// Save data to LocalStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <param name="value">object with the data to store</param>
        /// <returns></returns>
        public static async ValueTask LocalStorageSetAsync(this IJSRuntime jsRuntume, string key, object value)
            => await jsRuntume.InvokeVoidAsync("MyLocalStorage.Set", key, value);

        /// <summary>
        /// Remove data to LocalStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <returns></returns>
        public static async ValueTask LocalStorageDelAsync(this IJSRuntime jsRuntume, string key)
            => await jsRuntume.InvokeVoidAsync("MyLocalStorage.Del", key);
    }
}
