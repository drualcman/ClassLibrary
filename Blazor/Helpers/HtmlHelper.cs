using Microsoft.AspNetCore.Components;

namespace ClassLibrary.Helpers
{
    public static class HtmlHelper
    {
        public static MarkupString ToHtml(this string content)
        {
            MarkupString result = new MarkupString(content);
            return result;
        }
    }
}

