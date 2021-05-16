using Microsoft.AspNetCore.Components;
using System;
using System.Text.RegularExpressions;

namespace ClassLibrary.Helpers
{
    public static class HtmlHelper
    {
        /// <summary>
        /// Convert string into a HTML markup to show correctly in the page
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static MarkupString ToHtml(this string content)
        {
            string addBr = content.Replace(Environment.NewLine, "<br/>").Replace("\n\r", "<br/>");
            MarkupString result = new MarkupString(addBr);
            return result;
        }

        /// <summary>
        /// Remove HTML Tags to retrieve the clean text
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string StripHTML(this string content) => 
            Regex.Replace(content, "<.*?>", string.Empty);
    }
}

