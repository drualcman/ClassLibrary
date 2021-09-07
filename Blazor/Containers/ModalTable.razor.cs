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
        public List<string> DisplayNames { get; set; }
        [Parameter]
        public RenderFragment ChildContent { get; set; }
        [Parameter]
        public EventCallback OnSelect { get; set; }
        [Parameter]
        public EventCallback OnClose { get; set; }
        [Parameter]
        public EventCallback OnUpdate { get; set; }
        #endregion
        #region Methods
        void CloseModal() => OnClose.InvokeAsync();

        void UpdateList() => OnUpdate.InvokeAsync();

        void Select()
        {

        }
        #endregion
    }
}
