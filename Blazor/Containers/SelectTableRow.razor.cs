using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Containers
{
    public partial class SelectTableRow<T>
    {
        [Parameter]
        public IEnumerable<T> Items { get; set; }

        [Parameter]
        public RenderFragment<T> ChildContent { get; set; }

        [Parameter]
        public string RowClass { get; set; }

        [Parameter]
        public string RowClassSelected { get; set; }

        [Parameter]
        public T SelectedItem { get; set; }

        string SetSelected(T item)
        {
            if (SelectedItem is not null)
            {
                return SelectedItem.Equals(item) ? string.IsNullOrEmpty(RowClassSelected) ? "is-selected" : RowClassSelected : "";
            }
            else return string.Empty;
        }
    }
}
