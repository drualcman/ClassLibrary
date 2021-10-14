using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Threading.Tasks;

namespace ClassLibrary.Controls
{
    public partial class PopupConfirm
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        /// <summary>
        /// Delegate for the action agreed
        /// </summary>
        [Parameter]
        public EventCallback Agreed { get; set; }

        /// <summary>
        /// Delegate for not agreed
        /// </summary>
        [Parameter]
        public EventCallback NoOption { get; set; }

        /// <summary>
        /// Delegate for cancel action
        /// </summary>
        [Parameter]
        public EventCallback ClosePopup { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        /// <summary>
        /// Show or hide the popup
        /// </summary>
        [Parameter]
        public bool IsShow { get; set; }

        /// <summary>
        /// Not in use only Kenenth know how to use
        /// </summary>
        [Parameter]
        public string LoadingBtn { get; set; }

        /// <summary>
        /// If need to show loading action
        /// </summary>
        [Parameter]
        public bool IsLoading { get; set; }

        [Parameter]
        public string ActiveCss { get; set; }

        /// <summary>
        /// Hide CANCEL only show AGREED or NOT (User with a deletegat in call back)
        /// </summary>
        [Parameter]
        public bool ShowCancelOption { get; set; } = true;

        /// <summary>
        /// Hide the NOT AGREE, only AGREED or CANCEL (User with a deletegat in call back)
        /// </summary>
        [Parameter]
        public bool ShowNoOption { get; set; } = false;

        /// <summary>
        /// Setup teh text to show in the agreed button
        /// </summary>
        [Parameter]
        public string AgreedText { get; set; } = "YES";

        /// <summary>
        /// Setup teh text to show in the NOT agreed button
        /// </summary>
        [Parameter]
        public string NotAgreedText { get; set; } = "NO";

        /// <summary>
        /// Setup teh text to show in the cancel button
        /// </summary>
        [Parameter]
        public string CancelText { get; set; } = "CANCEL";
        [Parameter]
        public string YesBtnClass { get; set; }
        [Parameter]
        public string NoBtnClass { get; set; }
        [Parameter]
        public string CancelBtnClass { get; set; }

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
