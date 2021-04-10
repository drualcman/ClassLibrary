using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Javascript
{
    public static class JSRuntimeExtensions
    {
        /// <summary>
        /// Extent JSRuntime to get a confirm
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="message">message to show</param>
        /// <returns></returns>
        public static ValueTask<bool> Confirm(this IJSRuntime jsRuntime, string message) => 
            jsRuntime.InvokeAsync<bool>("confirm", message);

        /// <summary>
        /// Send Focus to a dom element
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="dom">Elemtn Id</param>
        /// <returns></returns>
        public static ValueTask Focus(this IJSRuntime jsRuntime, string dom)=> 
            jsRuntime.InvokeVoidAsync("$p.Focus", dom);

        /// <summary>
        /// Get browser lang
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public async static Task<string> Lang(this IJSRuntime jsRuntime)
        {
            try
            {
                string lang = await jsRuntime.InvokeAsync<string>("getLanguage");
                string[] langs = lang.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                string[] userLang = langs[0].Split('-');
                return userLang[0].ToUpper();
            }
            catch
            {
                return "es";
            }
        }

        /// <summary>
        /// Get browser time zone
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static ValueTask<string> TimeZone(this IJSRuntime jsRuntime) => 
            jsRuntime.InvokeAsync<string>("getBrowserTimeZoneIdentifier");

        /// <summary>
        /// Get browser time zone offset
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static ValueTask<string> TimeZoneOffset(this IJSRuntime jsRuntime) => 
            jsRuntime.InvokeAsync<string>("getBrowserTimeZoneOffset");
    }
}
