using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Controls
{
    public partial class PaginationFooter
    {
        #region basics
        [Parameter] public int TotalPages { get; set; }
        [Parameter] public int CurrentPage { get; set; }
        [Parameter] public EventCallback<int> OnChange { get; set; }

        void ToPage(int page)
        {
            OnChange.InvokeAsync(page);
        }
        #endregion

        #region Personalize
        [Parameter] public string ContainerCss { get; set; }
        [Parameter] public string ElementCss { get; set; }
        #endregion
    }
}
