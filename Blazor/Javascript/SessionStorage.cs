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
        /// Save data to SessionStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <param name="value">object with the data to store</param>
        /// <returns></returns>
        public static async ValueTask SessionStorageSetAsync(this IJSRuntime jsRuntume, string key, object value) => 
            await jsRuntume.InvokeVoidAsync("MySessionStorage.Set", key, value);

        /// <summary>
        /// Remove data to SessionStorage
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="key">name of the variable in the LocalStorage</param>
        /// <returns></returns>
        public static async ValueTask SessionStorageDelAsync(this IJSRuntime jsRuntume, string key)
            => await jsRuntume.InvokeVoidAsync("MySessionStorage.Del", key);
    }
}
