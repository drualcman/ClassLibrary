using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary.Containers
{
    public partial class InfiniteSlides<TItem>
    {
        [Parameter]
        public List<TItem> Items { get; set; }

        [Parameter]
        public RenderFragment Loading { get; set; }

        [Parameter]
        public string BodyContainerCss { get; set; }

        [Parameter]
        public RenderFragment<TItem> Body { get; set; }
        
        [Parameter]
        public RenderFragment Empty { get; set; }

        [Parameter]
        public string ButtonsContainerCss { get; set; }

        [Parameter]
        public RenderFragment Left { get; set; }

        [Parameter]
        public string LeftContainerCss { get; set; }

        [Parameter]
        public RenderFragment Right { get; set; }

        [Parameter]
        public string RightContainerCss { get; set; }

        protected override void OnParametersSet()
        {
            ItemsList = new List<Item>();
            int order = 0;
            foreach (TItem item in Items)
            {
                ItemsList.Add(new Item
                {
                    Element = item,
                    Order = order
                });
                order++;
            }            
        }

        List<Item> ItemsList;

        void MoveRight()
        {
            int c = ItemsList.Count();
            for (int i = 0; i < c; i++)
            {
                ItemsList[i].Order = ItemsList[i].Order + 1;
                if (ItemsList[i].Order >= c) ItemsList[i].Order = 0;
            }
            ItemsList.Sort((a, b) => a.Order.CompareTo(b.Order));
        }

        void MoveLeft()
        {
            SortedList<int, Item> valuePairs = new SortedList<int, Item>();
            int c = ItemsList.Count() - 1;
            for (int i = c; i >= 0; i--)
            {
                ItemsList[i].Order = ItemsList[i].Order - 1;
                if (ItemsList[i].Order < 0) ItemsList[i].Order = c;
            }
            ItemsList.Sort((a, b) => a.Order.CompareTo(b.Order));
        }
        private class Item
        {
            public TItem Element { get; set; }
            public int Order { get; set; }
        }
    }

}
