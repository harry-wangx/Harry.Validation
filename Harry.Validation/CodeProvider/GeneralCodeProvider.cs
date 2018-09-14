using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Validation.CodeProvider
{
    public class GeneralCodeProvider : ICodeProvider
    {
        private readonly Random rand = new Random();
        private readonly GeneralCodeOptions options;

        public GeneralCodeProvider() : this(new GeneralCodeOptions()) { }
        public GeneralCodeProvider(GeneralCodeOptions options)
        {
            this.options = options;
        }

        public Code Create()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < options.CodeLength; i++)
            {
                sb.Append(options.CodeChars[rand.Next(0, options.CodeChars.Length)]);
            }
            var str = sb.ToString();
            return new Code(str, str);
        }
    }
}
