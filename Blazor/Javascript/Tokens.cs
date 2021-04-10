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
        /// <summary>
        /// Get the claims about the active user and return the user model
        /// Authentication using jwt authentication
        /// </summary>
        /// <param name="jsRuntime"></param>
        /// <returns></returns>
        public static async Task<string> GetUserTokenAsync(this IJSRuntime jsRuntime)
        {
            return await jsRuntime.LocalStorageGetAsync("token");
        }
    }
}
