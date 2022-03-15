using System;

namespace ClassLibrary.Attributes
{
    public class DisplayTableAttribute : Attribute
    {
        public string TableClass { get; set; }
        public string Header { get; set; }
        public string HeaderClass { get; set; }
        public string ColClass { get; set; }
        public string ValueFormat { get; set; }
    }
}
