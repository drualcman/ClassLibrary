using ClassLibrary.Attributes;
using ClassLibrary.Controls.PaginationLists;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace ClassLibrary.Containers
{
    /// <summary>
    /// Show a table from a model. Must be a list class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Table<T> : ComponentBase
    {
        #region Main Parameters
        /// <summary>
        /// Set directly the items
        /// </summary>
        [Parameter] public IEnumerable<T> Items { get; set; }
        /// <summary>
        /// Set a function to load the items
        /// </summary>
        [Parameter] public Func<Task<IEnumerable<T>>> Loader { get; set; }
        [Parameter] public RenderFragment Head { get; set; }
        [Parameter] public RenderFragment<T> Body { get; set; }
        [Parameter] public RenderFragment Loading { get; set; }
        [Parameter] public RenderFragment Empty { get; set; }
        /// <summary>
        /// Only one with this property
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)] public Dictionary<string, object> AdditionalAttributes { get; set; }
        #endregion

        #region pagging
        [Parameter] public string SelectCss { get; set; } = "select";

        private int PageSizeBK = 10;

        public int PageSize
        {
            get { return PageSizeBK; }
            set
            {
                PageSizeBK = value;
                if(Paged is not null) Paged = PagedList<T>.ToPagedList(Items, Paged.CurrentPage, PageSizeBK);
            }
        }

        PagedList<T> Paged;
        Task ToPage(int page) =>
            Task.FromResult(Paged = PagedList<T>.ToPagedList(Items, page, PageSizeBK));
        #endregion

        #region initializing
        protected override async Task OnParametersSetAsync()
        {
            await LoadItems();
        }

        protected override void OnParametersSet()
        {
            Console.WriteLine("1");
            if(Items is not null)
            {
                ToPage(Paged?.CurrentPage ?? 1);
                DefaultView();
            }
        }

        [Inject]
        public IJSRuntime JS { get; set; }

        string TableId = Guid.NewGuid().ToString();

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {

            IJSObjectReference js = await JS.InvokeAsync<IJSObjectReference>("import", "./_content/ClassLibrary/PaginationFooter.js");
            await js.InvokeVoidAsync("PaginationFooter.FitPaginationColSpam", TableId);
            await js.DisposeAsync();

        }
        #endregion

        #region Rows
        /// <summary>
        /// Custom class for table row, put in here
        /// </summary>
        [Parameter] public string RowCss { get; set; }
        /// <summary>
        /// Custom class for active row
        /// </summary>
        [Parameter] public string RowCssSelected { get; set; }
        /// <summary>
        /// Update the selecteditem for the parent component get the change
        /// </summary>
        [Parameter] public EventCallback<T> OnClick { get; set; }
        /// <summary>
        /// Invoke a method to use the selected item
        /// </summary>
        [Parameter] public EventCallback<T> OnDoubleClick { get; set; }

        T SelectedItem;

        /// <summary>
        /// add active class for the row of the selected item 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        string SetSelected(T item)
        {
            if(SelectedItem is not null)
            {
                return SelectedItem.Equals(item) ? string.IsNullOrEmpty(RowCssSelected) ? "is-selected" : RowCssSelected : string.Empty;
            }
            else return string.Empty;
        }

        async ValueTask Row_Click(T item)
        {
            SelectedItem = item;
            if(OnClick.HasDelegate) await OnClick.InvokeAsync(SelectedItem);
        }

        async ValueTask Row_DoubleClick(T item)
        {
            SelectedItem = item;
            if(OnDoubleClick.HasDelegate) await OnDoubleClick.InvokeAsync(item);
        }
        #endregion

        #region Methods
        async Task LoadItems()
        {
            if(Loader is not null && Items is null)
            {
                Items = await Loader();
                await ToPage(Paged?.CurrentPage ?? 1);
                DefaultView();
            }
        }
        #endregion

        #region helpers
        MarkupString DefaultHead;
        MarkupString DefaultBody;
        private const string DefaultCSSClass = "table is-bordered is-striped is-hoverable is-fullwidth";

        void DefaultView()
        {
            if(Paged is not null)
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

                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public |           //get public names
                                                                    BindingFlags.Instance);         //get instance names


                //get all my attributes
                DisplayTableAttribute[] attributes = new DisplayTableAttribute[properties.Length];

                StringBuilder html;
                if(Head == null)
                {
                    html = new StringBuilder();
                    for(int i = 0; i < properties.Length; i++)
                    {
                        attributes[i] = properties[i].GetCustomAttribute<DisplayTableAttribute>();                  //get if my custom attributes
                                                                                                                    //custom header class
                        string OpenTHTag = attributes[i] != null && attributes[i].HeaderClass != null ? $"<th class=\"{attributes[i].HeaderClass}\">" : "<th>";
                        //custom header name
                        Attribute alias = Attribute.GetCustomAttribute(properties[i], typeof(DisplayAttribute));     //get if have attribute display to change the name of the property                    
                        string header = attributes[i] != null && attributes[i].Header != null ? attributes[i].Header :      //custom header name
                                        alias == null ? properties[i].Name : ((DisplayAttribute)alias).GetName();         //if not get the display attribute or name
                        html.Append($"{OpenTHTag}{header}</th>");
                    }
                    DefaultHead = new MarkupString(html.ToString());
                }
                if(Body == null)
                {
                    html = new StringBuilder();
                    //get all the item to show the values
                    foreach(T item in Paged)
                    {
                        html.Append("<tr>");
                        //show the values
                        for(int i = 0; i < properties.Length; i++)
                        {
                            attributes[i] = properties[i].GetCustomAttribute<DisplayTableAttribute>();                  //get if my custom attributes
                            string OpenTDTag = attributes[i] != null && attributes[i].ColClass != null ? $"<td class=\"{attributes[i].ColClass}\">" : "<td>";
                            var value = attributes[i] != null && attributes[i].ValueFormat != null ? string.Format(attributes[i].ValueFormat, properties[i].GetValue(item)) : properties[i].GetValue(item);
                            html.Append($"{OpenTDTag}{value}</td>");
                        }
                        html.Append("</tr>");
                    }
                    DefaultBody = new MarkupString(html.ToString());
                }
            }
        }
        #endregion
    }
}
