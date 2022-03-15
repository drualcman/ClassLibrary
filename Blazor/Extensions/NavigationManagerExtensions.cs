using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;

namespace ClassLibrary.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static string GetQueryString(this NavigationManager navManager, string key)
        {
            Uri uri = navManager.ToAbsoluteUri(navManager.Uri);
            try
            {
                return QueryHelpers.ParseNullableQuery(uri.Query)[key];
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
