using Harry.Validation.ImageProvider;
using NUnit.Framework;
using System;
using System.Drawing.Imaging;

namespace Harry.Validation.Test
{
    public class ImageFactoryTest
    {
        [Test]
        public void Create()
        {
            IImageFactory fac = new ImageFactory();
            fac.AddProvider(new GeneralImageProvider(new GeneralImageOptions()));

            fac.Create(110, 40, "abcde").Save("abcde.jpg",ImageFormat.Jpeg);

            Assert.True(true);
        }
    }
}
