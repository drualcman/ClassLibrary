using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Javascript
{
    public enum BoxType
    {
        Info,
        Succes,
        Warninig,
        Danger,
        Error,
        Waiting
    }


    public static class Messages
    {
        #region bulma
        /// <summary>
        /// Show Bulma Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="message">message to show</param>
        /// <returns></returns>
        public static async ValueTask BulmaMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string message) =>
            await BulmaMessageAsync(jsRuntume, type, target, message);

        /// <summary>
        /// Show Bulma Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="title">title for the message</param>
        /// <param name="message">message to show</param>
        /// <returns></returns>
        public static async ValueTask BulmaMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string title, string message) =>
            await BulmaMessageAsync(jsRuntume, type, target, title, message, 0);

        /// <summary>
        /// Show Bulma Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="title">title for the message</param>
        /// <param name="message">message to show</param>
        /// <param name="timeout">timeout to out hide. 0 never hide</param>
        /// <returns></returns>
        public static async ValueTask BulmaMessageAsync(this IJSRuntime jsRuntume, BoxType type, 
            string target, string title, string message, int timeout) 
        {
            switch (type)
            {
                case BoxType.Succes:
                    await jsRuntume.InvokeVoidAsync("$p.MsgSuccess", target, title, message, timeout, true);
                    break;
                case BoxType.Warninig:
                    await jsRuntume.InvokeVoidAsync("$p.MsgWarning", target, title, message, timeout, true);
                    break;
                case BoxType.Danger:
                case BoxType.Error:
                    await jsRuntume.InvokeVoidAsync("$p.MsgError", target, title, message, timeout, true);
                    break;
                case BoxType.Waiting:
                    await jsRuntume.InvokeVoidAsync("$p.MsgWaiting", target, message, timeout);
                    break;
                case BoxType.Info:
                default:
                    await jsRuntume.InvokeVoidAsync("$p.MsgInfo", target, title, message, timeout, true);
                    break;
            }
        }

        /// <summary>
        /// Show Bulma Waiting icon
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <returns></returns>
        public static async ValueTask BulmaWaitingAsync(this IJSRuntime jsRuntume, string target) =>
            await BulmaWaitingAsync(jsRuntume, target, "");

        /// <summary>
        /// Show Bulma Waiting icon
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="message">message to show</param>
        /// <returns></returns>
        public static async ValueTask BulmaWaitingAsync(this IJSRuntime jsRuntume, string target, string message) =>
            await BulmaWaitingAsync(jsRuntume, target, "", 0);

        /// <summary>
        /// Show Bulma Waiting icon
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="message">message to show</param>
        /// <param name="timeout">timeout to out hide. 0 never hide</param>
        /// <returns></returns>
        public static async ValueTask BulmaWaitingAsync(this IJSRuntime jsRuntume, string target, string message, int timeout) =>
            await jsRuntume.InvokeVoidAsync("$p.MsgWaiting", target, message, timeout);

        /// <summary>
        /// Remove Bulma Waiting icon
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <returns></returns>
        public static async ValueTask BulmaDeleteWaitingAsync(this IJSRuntime jsRuntume, string target) =>
            await jsRuntume.InvokeVoidAsync("$p.MsgWaitingDelete", target);
        #endregion

        #region bootstrap
        /// <summary>
        /// Show Bootstrap Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="message">message to show</param>
        /// <returns></returns>
        public static async ValueTask BootstrapMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string message)=>
            await BootstrapMessageAsync(jsRuntume, type, target, message);

        /// <summary>
        /// Show Bootstrap Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="title">title for the message</param>
        /// <param name="message">message to show</param>
        /// <returns></returns>
        public static async ValueTask BootstrapMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string title, string message) =>
            await BootstrapMessageAsync(jsRuntume, type, target, title, message, 0);

        /// <summary>
        /// Show Bootstrap Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="title">title for the message</param>
        /// <param name="message">message to show</param>
        /// <param name="timeout">timeout to out hide. 0 never hide</param>
        /// <returns></returns>
        public static async ValueTask BootstrapMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string title, string message, int timeout)
        {
            switch (type)
            {
                case BoxType.Succes:
                    await jsRuntume.InvokeVoidAsync("$p.MsgSuccess", target, title, message, timeout, false);
                    break;
                case BoxType.Warninig:
                    await jsRuntume.InvokeVoidAsync("$p.MsgWarning", target, title, message, timeout, false);
                    break;
                case BoxType.Danger:
                case BoxType.Error:
                    await jsRuntume.InvokeVoidAsync("$p.MsgError", target, title, message, timeout, false);
                    break;
                case BoxType.Waiting:
                    await jsRuntume.InvokeVoidAsync("$p.MsgInfo", target, "", message, timeout, false);
                    break;
                case BoxType.Info:
                default:
                    await jsRuntume.InvokeVoidAsync("$p.MsgInfo", target, title, message, timeout, false);
                    break;
            }
        }
        #endregion
    }
}
