using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Validation.CodeProvider
{
    public class GeneralCodeOptions
    {
        public string CodeChars
        {
            get;
            set;
        } = "ABCDEFGHJKLMNPQRSTUVWXYZ123467890";

        public int CodeLength { get; set; } = 5;
    }
}
