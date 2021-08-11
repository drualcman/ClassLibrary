using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace ClassLibrary.Controls
{
    public partial class PopupConfirm
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public EventCallback Agreed { get; set; }

        [Parameter]
        public EventCallback ClosePopup { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool IsShow { get; set; }

        [Parameter]
        public string LoadingBtn { get; set; }

        [Parameter]
        public bool IsLoading { get; set; }

        [Parameter]
        public string ActiveCss { get; set; }

        protected override void OnParametersSet()
        {
            ActiveCss = ActiveCss ?? "is-active";
        }

        async Task Close() => await ClosePopup.InvokeAsync();

        void Continue() => Agreed.InvokeAsync();

        string IsLoadingBtn() => IsLoading || (LoadingBtn == "popupsubmit") ? "cst-loading" : "";
    }
}
