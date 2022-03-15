using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ClassLibrary.Javascript
{
    /// <summary>
    /// Interop with JAVASCRIPT to manage Cookies
    /// </summary>
    public static class Cookies
    {
        /// <summary>
        /// Recovery data from Cookies
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="name">name of the cookie</param>
        /// <returns></returns>
        public static ValueTask<string> CookiesGetAsync(this IJSRuntime jsRuntume, string name)
            => jsRuntume.InvokeAsync<string>("Cookies.Get"                //function to execute from C# interop
                                        , name);

        /// <summary>
        /// Save data to Cookies
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="name">name of the of the cookie</param>
        /// <param name="value">object with the data to store</param>
        /// <param name="days">expire in x days</param>
        /// <returns></returns>
        public static ValueTask CookiesSetAsync(this IJSRuntime jsRuntume, string name, object value, int days)
            => jsRuntume.InvokeVoidAsync("Cookies.Set"                //function to execute from C# interop
                                        , name, value, days);

        /// <summary>
        /// Save data to Cookies
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="name">name of the of the cookie</param>
        /// <param name="value">object with the data to store</param>
        /// <param name="days">expire in x days</param>
        /// <param name="path">path to use</param>
        /// <returns></returns>
        public static ValueTask CookiesSetAsync(this IJSRuntime jsRuntume, string name, object value, int days, string path)
            => jsRuntume.InvokeVoidAsync("Cookies.Set"                //function to execute from C# interop
                                        , name, value, days, path);

        /// <summary>
        /// Remove data to Cookies
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="name">name of the cookie</param>
        /// <returns></returns>
        public static ValueTask CookiesDeleteAsync(this IJSRuntime jsRuntume, string name)
            => jsRuntume.InvokeVoidAsync("Cookies.Del"                //function to execute from C# interop
                                        , name);

        /// <summary>
        /// Remove data to Cookies
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="name">name of the cookie</param>
        /// <param name="path">name of the variable in the LocalStorage</param>
        /// <returns></returns>
        public static ValueTask CookiesDeleteAsync(this IJSRuntime jsRuntume, string name, string path)
            => jsRuntume.InvokeVoidAsync("Cookies.Del"                //function to execute from C# interop
                                        , name, path);

        /// <summary>
        /// Check if exist the Cookies
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="name">name of the cookie</param>
        /// <returns></returns>
        public static ValueTask CookiesCheckAsync(this IJSRuntime jsRuntume, string name)
            => jsRuntume.InvokeVoidAsync("Cookies.Check"                //function to execute from C# interop
                                        , name);

    }
}
