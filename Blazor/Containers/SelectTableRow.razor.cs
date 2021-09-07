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
        public List<T> Items { get; set; }
        [Parameter]
        public RenderFragment<T> Body { get; set; }
    }
}
