using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static string GetQueryString(this NavigationManager navManager, string key)
        {
            Uri uri = navManager.ToAbsoluteUri(navManager.Uri);
            return QueryHelpers.ParseNullableQuery(uri.Query)[key];
        }
    }
}
