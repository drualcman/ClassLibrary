using Microsoft.AspNetCore.Components;
using System;
using System.Text;
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
            string addBr = content.Replace(Environment.NewLine, "<BR />").Replace("\n\r", "<BR />");
            MarkupString result = new MarkupString(addBr);
            return result;
        }

        /// <summary>
        /// Convert string into a HTML markup to show correctly in the page
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static MarkupString ToHtml(this string content, string code, string replace)
        {            
            return ToHtml(content, new string[] { code}, new string[] { replace});
        }

        /// <summary>
        /// Convert string into a HTML markup to show correctly in the page
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static MarkupString ToHtml(this string content, string[] code, string[] replace)
        {
            if (code.Length == replace.Length)
            {
                int c = code.Length;
                for (int i = 0; i < c; i++)
                {
                    content = content.Replace(code[i], replace[i]);
                }
            }
            return ToHtml(content);
        }

        /// <summary>
        /// Remove HTML Tags to retrieve the clean text
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string StripHTML(this string content) => 
            Regex.Replace(content, "<.*?>", string.Empty).Replace("&nbsp;", " ");
    }
}

