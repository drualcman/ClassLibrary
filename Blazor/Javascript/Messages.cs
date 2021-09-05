using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ClassLibrary.Javascript
{
    public enum BoxType
    {
        Info,
        Success,
        Warning,
        Danger,
        Error,
        Waiting,
        Default,
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
        public static void BulmaMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string message) =>
            BulmaMessageAsync(jsRuntume, type, target, "", message);

        /// <summary>
        /// Show Bulma Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="title">title for the message</param>
        /// <param name="message">message to show</param>
        /// <returns></returns>
        public static void BulmaMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string title, string message) =>
            BulmaMessageAsync(jsRuntume, type, target, title, message, 0);

        /// <summary>
        /// Show Bulma Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="title">title for the message</param>
        /// <param name="message">message to show</param>
        /// <param name="timeout">timeout to out hide. 0 never hide</param>
        /// <returns></returns>
        public static async void BulmaMessageAsync(this IJSRuntime jsRuntume, BoxType type, 
            string target, string title, string message, int timeout) 
        {
            switch (type)
            {
                case BoxType.Success:
                    await jsRuntume.InvokeVoidAsync("$p.MsgSuccess", target, title, message, timeout, true);
                    break;
                case BoxType.Warning:
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
        public static void BootstrapMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string message)=>
            BootstrapMessageAsync(jsRuntume, type, target, "", message);

        /// <summary>
        /// Show Bootstrap Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="title">title for the message</param>
        /// <param name="message">message to show</param>
        /// <returns></returns>
        public static  void BootstrapMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string title, string message) =>
            BootstrapMessageAsync(jsRuntume, type, target, title, message, 0);

        /// <summary>
        /// Show Bootstrap Message
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="target">selector where show the message</param>
        /// <param name="title">title for the message</param>
        /// <param name="message">message to show</param>
        /// <param name="timeout">timeout to out hide. 0 never hide</param>
        /// <returns></returns>
        public static async void BootstrapMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string title, string message, int timeout)
        {
            switch (type)
            {
                case BoxType.Success:
                    await jsRuntume.InvokeVoidAsync("$p.MsgSuccess", target, title, message, timeout, false);
                    break;
                case BoxType.Warning:
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

        #region custom
        /// <summary>
        /// Show Custom Message on the page
        /// </summary>
        /// <param name="type">status box to show</param>
        /// <param name="target">parent element to eppend</param>
        /// <param name="message">notification content</param>
        /// <returns></returns>
        public static void CustomMessageAsync(this IJSRuntime jsRuntime, BoxType type, string target, string message) =>
               CustomMessageAsync(jsRuntime, type, target, "", message, 0, string.Empty);

        /// <summary>
        /// Show Custom Message on the page
        /// </summary>
        /// <param name="type">status box to show</param>
        /// <param name="target">parent element to eppend</param>
        /// <param name="title">title</param>
        /// <param name="message">notification content</param>
        /// <returns></returns>
        public static void CustomMessageAsync(this IJSRuntime jsRuntime, BoxType type, string target, string title, string message) =>
               CustomMessageAsync(jsRuntime, type, target, title, message, 0, string.Empty);

        /// <summary>
        /// Show Custom Message on the page
        /// </summary>
        /// <param name="target">parent element to eppend</param>
        /// <param name="title">title</param>
        /// <param name="message">notification content</param>
        /// <returns></returns>
        public static void CustomMessageAsync(this IJSRuntime jsRuntime, string target, string title, string message) =>
               CustomMessageAsync(jsRuntime, BoxType.Default, target, title, message, 0, string.Empty);

        /// <summary>
        /// Show Custom Message on the page
        /// </summary>
        /// <param name="target">parent element to eppend</param>
        /// <param name="message">notification content</param>
        /// <returns></returns>
        public static void CustomMessageAsync(this IJSRuntime jsRuntime, string target, string message) =>
               CustomMessageAsync(jsRuntime, BoxType.Default, target, string.Empty, message, 0, string.Empty);

        /// <summary>
        /// Show Custom Message on the page
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="type">status box to show</param>
        /// <param name="target">parent element to eppend</param>
        /// <param name="title">title</param>
        /// <param name="message">notification content</param>
        /// <param name="timeout">notification remove time in seconds</param>
        /// <param name="classes">optional, additional classes</param>
        /// <returns></returns>
        public static async void CustomMessageAsync(this IJSRuntime jsRuntume, BoxType type,
            string target, string title, string message, int timeout, string classes = "")
        {
            switch (type)
            {
                case BoxType.Success:
                    await jsRuntume.InvokeVoidAsync("CustomMessage", target, title, message, "is-success", timeout, classes);
                    break;
                case BoxType.Warning:
                    await jsRuntume.InvokeVoidAsync("CustomMessage", target, title, message, "is-warning", timeout, classes);
                    break;
                case BoxType.Danger:
                case BoxType.Error:
                    await jsRuntume.InvokeVoidAsync("CustomMessage", target, title, message, "is-danger", timeout, classes);
                    break;
                case BoxType.Waiting:
                    await jsRuntume.InvokeVoidAsync("CustomMessage", target, title, message, "is-waiting", timeout, classes);
                    break;
                case BoxType.Info:
                    await jsRuntume.InvokeVoidAsync("CustomMessage", target, title, message, "is-info", timeout, classes);
                    break;
                default:
                    await jsRuntume.InvokeVoidAsync("CustomMessage", target, title, message, "is-default", timeout, classes);
                    break;
            }
        }
        #endregion

        #region custom popup
        /// <summary>
        /// Show a small popup on the bottom right of the screen
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="message">message content</param>
        /// <param name="status">popup status ex. success, danger</param>
        /// <returns></returns>
        public static void PopupResponseAsync(this IJSRuntime jsRuntume, string message, bool status)
        {
             PopupResponseAsync(jsRuntume, message, status, null, null);
        }

        /// <summary>
        /// Show a small popup on the bottom right of the screen
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="message">message content</param>
        /// <param name="status">popup status ex. success, danger</param>
        /// <param name="time">time limit on showing the popup</param>
        /// <returns></returns>
        public static void PopupResponseAsync(this IJSRuntime jsRuntume, string message, bool status, int time)
        {
            PopupResponseAsync(jsRuntume, message, status, time, null);
        }

        /// <summary>
        /// Show a small popup on the bottom right of the screen
        /// </summary>
        /// <param name="jsRuntume"></param>
        /// <param name="message">message content</param>
        /// <param name="status">popup status ex. success, danger</param>
        /// <param name="time">time limit on showing the popup</param>
        /// <param name="additional">additional popup design</param>
        /// <returns></returns>
        public static async void PopupResponseAsync(this IJSRuntime jsRuntume, string message, bool status, int? time, string additional)
        {
            await jsRuntume.InvokeVoidAsync("PopupResponse", message, status, time, additional);
        }
        #endregion
    }
}
