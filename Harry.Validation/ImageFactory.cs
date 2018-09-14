using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Harry.Validation
{
    public class ImageFactory : IImageFactory
    {
        private readonly Random r = new Random();
        private readonly List<IImageProvider> providers = new List<IImageProvider>();

        public ImageFactory()
        {

        }

        public ImageFactory(IEnumerable<IImageProvider> providers)
        {
            this.providers.AddRange(providers);
        }
        public IImageFactory AddProvider(IImageProvider provider)
        {
            providers.Add(provider);
            return this;
        }

        public Bitmap Create(int width, int height, string txt)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), "width值不能小于0");
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), "height值不能小于0");
            if (string.IsNullOrEmpty(txt))
                throw new ArgumentNullException(nameof(txt));

            if (providers.Count <= 0)
                throw new Exception("未注册任何IImageProvider");
            return providers[r.Next(0, providers.Count)].Create(width, height, txt);
        }
    }
}
