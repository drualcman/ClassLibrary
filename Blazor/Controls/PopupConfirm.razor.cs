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
        public EventCallback NoOption { get; set; }

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

        /// <summary>
        /// Cancel Option is shown by default
        /// </summary>
        [Parameter]
        public bool ShowCancelOption { get; set; } = true;

        /// <summary>
        /// No Option is not shown as default
        /// </summary>
        [Parameter]
        public bool ShowNoOption { get; set; } = false;

        protected override void OnParametersSet()
        {
            ActiveCss = ActiveCss ?? "is-active";
        }

        async Task Close() => await ClosePopup.InvokeAsync();
        async Task No() => await NoOption.InvokeAsync();
        void Continue() => Agreed.InvokeAsync();

        string IsLoadingBtn() => IsLoading || (LoadingBtn == "popupsubmit") ? "cst-loading" : "";
    }
}
