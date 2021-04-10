using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Attributes
{
    public class Identity: Attribute
    {
        public bool DisplayName { get; set; } = false;
        public bool ClaimIgnore { get; set; } = true;
    }
}
