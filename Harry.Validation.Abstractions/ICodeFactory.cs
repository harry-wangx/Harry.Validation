using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Harry.Validation
{
    public interface ICodeFactory
    {
        ICodeFactory AddProvider(ICodeProvider provider);

        Code Create();
    }
}
