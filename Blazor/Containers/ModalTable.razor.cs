using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Containers
{
    public partial class ModalTable
    {
        #region Properties
        [Parameter]
        public string TableId { get; set; }
        [Parameter]
        public string TableTitle { get; set; }
        [Parameter]
        public RenderFragment TableHeader { get; set; }
        [Parameter]
        public RenderFragment TableBody { get; set; }
        [Parameter]
        public RenderFragment TableFooter { get; set; }
        [Parameter]
        public EventCallback OnSelect { get; set; }
        [Parameter]
        public EventCallback OnClose { get; set; }
        [Parameter]
        public EventCallback OnUpdate { get; set; }

        #endregion

        bool IsShowingModal;

        #region Methods
        void CloseModal() => IsShowingModal = false;

        void UpdateList() => OnUpdate.InvokeAsync();

        void Cancel()
        {
            IsShowingModal = false;
            OnClose.InvokeAsync();
        }

        void Select()
        {
            IsShowingModal = false;
            OnSelect.InvokeAsync();
        }
        #endregion
    }
}
