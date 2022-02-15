using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;
using System.Text.Json;


namespace ClassLibrary.Javascript
{
    public static class AudioPlayer
    {

        const string AudioJavascript = "./_content/ClassLibrary/AudioPlayer.js";

        /// <summary>
        /// Play a sound
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ValueTask AudioPlayerPlay(this IJSRuntime jsRuntime, string fileName) =>
            AudioPlayerPlay(jsRuntime, fileName, "");

        /// <summary>
        /// Play a sound
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="fileName"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public static async ValueTask AudioPlayerPlay(this IJSRuntime jsRuntime, string fileName, string container)
        {
            IJSObjectReference audioPlayer = await jsRuntime.InvokeAsync<IJSObjectReference>("import", AudioJavascript);
            await audioPlayer.InvokeVoidAsync("AudioPlayer.Play", fileName, container);
            audioPlayer = null;
        }

        /// <summary>
        /// Stop playing a sound
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public static async ValueTask AudioPlayerStop(this IJSRuntime jsRuntime, string container)
        {
            IJSObjectReference audioPlayer = await jsRuntime.InvokeAsync<IJSObjectReference>("import", AudioJavascript);
            await audioPlayer.InvokeVoidAsync("AudioPlayer.Stop", container);
            audioPlayer = null;
        }
    }
}
