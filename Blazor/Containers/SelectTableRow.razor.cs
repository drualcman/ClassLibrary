using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace ClassLibrary.Containers
{
    public partial class SelectTableRow<T>
    {
        /// <summary>
        /// Itms to be loop and shown in the table list
        /// </summary>
        [Parameter]
        public IEnumerable<T> Items { get; set; }
        /// <summary>
        /// Put the td tag(table td) that you need
        /// </summary>
        [Parameter]
        public RenderFragment<T> ChildContent { get; set; }
        /// <summary>
        /// Custom class for table row, put in here
        /// </summary>
        [Parameter]
        public string RowClass { get; set; }
        /// <summary>
        /// Custom class for active row
        /// </summary>
        [Parameter]
        public string RowClassSelected { get; set; }
        /// <summary>
        /// Pass the variable for getting the active item or row
        /// </summary>
        [Parameter]
        public T SelectedItem { get; set; }
        /// <summary>
        /// Update the selecteditem for the parent component get the change
        /// </summary>
        [Parameter]
        public EventCallback<T> SelectItem { get; set; }
        /// <summary>
        /// Invoke a method to use the selected item
        /// </summary>
        [Parameter]
        public EventCallback OnDoubleClick { get; set; }

        /// <summary>
        /// add active class for the row of the selected item 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        string SetSelected(T item)
        {
            if(SelectedItem is not null)
            {
                return SelectedItem.Equals(item) ? string.IsNullOrEmpty(RowClassSelected) ? "is-selected" : RowClassSelected : "";
            }
            else return string.Empty;
        }

        void SelectTheItem(T item)
        {
            SelectedItem = item;
            SelectItem.InvokeAsync(SelectedItem);
        }

        void DoubleClick(T item)
        {
            SelectTheItem(item);
            OnDoubleClick.InvokeAsync();
        }
    }
}
