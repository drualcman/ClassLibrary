using ClassLibrary.Attributes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

/*
    how to use this component
     @if (myToppings == null)
    {
        <div class="loading-bar"></div>
    }
    else
    {
        <Table Items="myToppings">
            <Head>
                <th>Id</th>
                <th>Nombre</th>
            </Head>
            <Body>
                <td>@context.Id</td>
                <td>@context.Name</td>
            </Body>
        </Table>
    }
    @if (myPizzas == null)
    {
        <div class="loading-bar"></div>
    }
    else
    {
        <Table Items="myPizzas" />
    }
 */

namespace ClassLibrary.Containers
{
    /// <summary>
    /// Show a table from a model. Must be a list class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class Table<T> : ComponentBase
    {
        [Parameter]
        public IEnumerable<T> Items { get; set; }
        
        [Parameter]
        public RenderFragment Head { get; set; }

        [Parameter]
        public RenderFragment<T> Body { get; set; }

        [Parameter]
        public RenderFragment Loading { get; set; }

        [Parameter]
        public RenderFragment Empty { get; set; }

        /// <summary>
        /// Only one with this property
        /// </summary>
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; }

        MarkupString DefaultHead;
        MarkupString DefaultBody;
        private readonly string DefaultCSSClass = "table is-bordered is-striped is-hoverable is-fullwidth";

        protected override void OnParametersSet()
        {
            if (Items != null)
            {
                if (AdditionalAttributes == null)
                {
                    AdditionalAttributes = new Dictionary<string, object>();
                }
                if (!AdditionalAttributes.ContainsKey("class"))
                {
                    //check if have a table css class on the model send
                    DisplayTableAttribute cssClass = typeof(T).GetCustomAttribute<DisplayTableAttribute>();
                    if (cssClass != null && cssClass.TableClass != null) AdditionalAttributes.Add("class", cssClass.TableClass);
                    else AdditionalAttributes.Add("class", DefaultCSSClass);
                }

                PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public |           //get public names
                                                                    BindingFlags.Instance);         //get instance names


                //get all my attributes
                DisplayTableAttribute[] attributes = new DisplayTableAttribute[properties.Length];

                StringBuilder html;
                if (Head == null)
                {
                    html = new StringBuilder();
                    for (int i = 0; i < properties.Length; i++)
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
                if (Body == null)
                {
                    html = new StringBuilder();
                    //get all the item to show the values
                    foreach (T item in Items)
                    {
                        html.Append("<tr>");
                        //show the values
                        for (int i = 0; i < properties.Length; i++)
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
            else return;
        }
    }
}
