using Microsoft.AspNetCore.Components;

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
    }
}

