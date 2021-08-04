using Microsoft.AspNetCore.Components;

namespace ClassLibrary.Controls
{
    public partial class PopupConfirm
    {
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

        void Close() => ClosePopup.InvokeAsync();

        void Continue() => Agreed.InvokeAsync();

        string IsLoadingBtn() => IsLoading || (LoadingBtn == "popupsubmit") ? "cst-loading" : "";
    }
}
