using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Harry.Validation
{
    public interface IImageFactory
    {
        IImageFactory AddProvider(IImageProvider provider);

        Bitmap Create(int width, int height, string txt);
    }
}
