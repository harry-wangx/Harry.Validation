using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Validation
{
    public class Code
    {
        public Code() { }

        public Code(string displayText, string value)
        {
            this.DisplayText = displayText;
            this.Value = value;
        }
        public string DisplayText { get; protected set; }

        public string Value { get; protected set; }

        public virtual bool Validate(string input)
        {
            return String.Equals(Value, input, StringComparison.OrdinalIgnoreCase);
        }
    }
}
