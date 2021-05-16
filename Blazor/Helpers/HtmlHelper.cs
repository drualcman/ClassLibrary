using Microsoft.AspNetCore.Components;
using System;
using System.Text.RegularExpressions;

namespace ClassLibrary.Helpers
{
    public static class HtmlHelper
    {
        public static MarkupString ToHtml(this string content)
        {
            string addBr = content.Replace(System.Environment.NewLine, "<br/>").Replace("\n\r", "<br/>");
            MarkupString result = new MarkupString(addBr);
            return result;
        }

        public static string StripHTML(this string content) => Regex.Replace(content, "<.*?>", string.Empty);
    }
}

