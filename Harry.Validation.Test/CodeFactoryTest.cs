using Harry.Validation.CodeProvider;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Harry.Validation.Test
{
    public class CodeFactoryTest
    {
        [Test]
        public void Create()
        {
            ICodeFactory fac = new CodeFactory();
            fac.AddProvider(new GeneralCodeProvider(new GeneralCodeOptions()));

            var code= fac.Create();

            Assert.True(code.Value.Length==5);
            Assert.True(code.Validate(code.DisplayText));
        }
    }
}
