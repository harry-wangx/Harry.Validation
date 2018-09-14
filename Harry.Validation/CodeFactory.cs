using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Harry.Validation
{
    public class CodeFactory : ICodeFactory
    {
        private readonly Random r = new Random();
        private readonly List<ICodeProvider> providers = new List<ICodeProvider>();

        public CodeFactory()
        {

        }

        public CodeFactory(IEnumerable<ICodeProvider> providers)
        {
            this.providers.AddRange(providers);
        }

        public ICodeFactory AddProvider(ICodeProvider provider)
        {
            providers.Add(provider);
            return this;
        }

        public Code Create()
        {
            if (providers.Count <= 0)
                throw new Exception("未注册任何ICodeProvider");
            return providers[r.Next(0, providers.Count)].Create();
        }
    }
}
