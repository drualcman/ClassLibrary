using System;

namespace ClassLibrary.Attributes
{
    public class Identity : Attribute
    {
        public bool DisplayName { get; set; } = false;
        public bool ClaimIgnore { get; set; } = true;
    }
}
