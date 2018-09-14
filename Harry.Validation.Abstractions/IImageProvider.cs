using System;
using System.Drawing;

namespace Harry.Validation
{
    public interface IImageProvider
    {
        Bitmap Create(int width, int height, string txt);
    }
}
