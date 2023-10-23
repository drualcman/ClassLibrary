using ClassLibrary.Attributes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ClassLibrary.Containers
{
    public partial class Select<T> : ComponentBase
    {
        [Parameter]
        public string DefaultOptionText { get; set; }

        [Parameter]
        public IEnumerable<T> Items { get; set; }

        [Parameter]
        public string DataField { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        [Parameter]
        public EventCallback<T> OnChange { get; set; }

        //MarkupString SelectOption;
        private readonly string DefaultCSSClass = "select";

        protected override void OnParametersSet()
        {
            if(AdditionalAttributes == null)
            {
                AdditionalAttributes = new Dictionary<string, object>();
            }
            if(!AdditionalAttributes.ContainsKey("class"))
            {
                //check if have a table css class on the model send
                DisplayTableAttribute cssClass = typeof(T).GetCustomAttribute<DisplayTableAttribute>();
                if(cssClass != null && cssClass.TableClass != null) AdditionalAttributes.Add("class", cssClass.TableClass);
                else AdditionalAttributes.Add("class", DefaultCSSClass);
            }
            if(Items != null)
            {
                StringBuilder html = new StringBuilder();
                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                if(properties.Length > 2)
                {
                    foreach(T item in Items)
                    {

                    }
                }
            }
        }

        object GetValue(int index)
        {
            object result;
            PropertyInfo property = string.IsNullOrEmpty(DataField) ? typeof(T).GetProperties()[0] : typeof(T).GetProperty(DataField);
            property = property ?? typeof(T).GetProperties()[0];            //ensure have some property
            result = property.GetValue(Items.ElementAt(index));
            return result;
        }

        void Change(ChangeEventArgs e) => OnChange.InvokeAsync(Items.ElementAt(Convert.ToInt32(e.Value)));
    }
}
