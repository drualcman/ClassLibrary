using ClassLibrary.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ClassLibrary.Controls
{
    public partial class SearchOnTable
    {
        [Inject]
        public IDefaultServices Services { get; set; }

        [Parameter]
        public string Table { get; set; }
        [Parameter]
        public string SearchInputUniqueClass { get; set; }

        string SearchText;

        #region methods
        async Task FilterTable()
        {
            await Services.JsRuntime.InvokeVoidAsync("$p.SearchInTable", Table, SearchText);
        }

        public async Task Clear()
        {
            SearchText = "";
            await FilterTable();
            StateHasChanged();
        }
        #endregion
    }
}
