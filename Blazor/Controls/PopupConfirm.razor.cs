using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ClassLibrary.Controls
{
    public partial class PopupConfirm
    {
        [Parameter]
        public EventCallback Agreed { get; set; }

        [Parameter]
        public EventCallback ClosePopup { get; set; }

        [Parameter]
        public bool IsShow { get; set; }

        [Parameter]
        public string LoadingBtn { get; set; }

        protected override Task OnParametersSetAsync()
        {
            LoadingBtn = LoadingBtn ?? "";
            return base.OnParametersSetAsync();
        }

        void Close() => ClosePopup.InvokeAsync();

        void Continue() => Agreed.InvokeAsync();
    }
}
