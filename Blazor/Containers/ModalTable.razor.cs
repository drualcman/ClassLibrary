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
        /// <summary>
        /// Give id to the table for use of SearchOnTable component
        /// </summary>
        [Parameter]
        public string TableId { get; set; }
        [Parameter]
        public string TableTitle { get; set; }
        /// <summary>
        /// content to show in a button to open a popup, default contect a magnifier
        /// </summary>
        [Parameter]
        public RenderFragment ButtonContent { get; set; }
        /// <summary>
        /// put the table heads in this fragment
        /// </summary>
        [Parameter]
        public RenderFragment TableHeader { get; set; }
        /// <summary>
        /// put the loop of list, use the SelecTableRow component
        /// </summary>
        [Parameter]
        public RenderFragment TableBody { get; set; }
        /// <summary>
        /// Put all what you want to put on table footer in here 
        /// </summary>
        [Parameter]
        public RenderFragment TableFooter { get; set; }
        /// <summary>
        /// Invoke a method when select button is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnSelect { get; set; }
        /// <summary>
        /// invoke a method when cancel button is clicked
        /// </summary>
        [Parameter]
        public EventCallback OnClose { get; set; }
        /// <summary>
        /// Invoke a method when update button is clicked. this is use to update the indexed  db from the live database
        /// </summary>
        [Parameter]
        public EventCallback OnUpdate { get; set; }
        /// <summary>
        /// Set the disabled attribute of the button
        /// </summary>
        [Parameter]
        public bool IsButtonDisabled { get; set; }
        /// <summary>
        /// Update Button Text
        /// </summary>
        [Parameter]
        public string UpdateText { get; set; } = "DB Sync";

        [Parameter]
        public string CssClass { get; set; } = "button is-default fc-6 fc-6";

        #endregion
        #region MyRegion
        bool IsShowingModal;
        #endregion
        #region Methods
        public void CloseModal() => IsShowingModal = false;

        void Cancel()
        {
            OnClose.InvokeAsync();
        }

        void Select() => OnSelect.InvokeAsync();
        #endregion
    }
}
