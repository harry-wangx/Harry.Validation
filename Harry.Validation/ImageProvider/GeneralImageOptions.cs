using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Harry.Validation.ImageProvider
{
    public class GeneralImageOptions
    {
        public List<string> RandomFontFamily = new List<string>()
        {
                "arial",
                "arial black",
                "comic sans ms",
                "courier new",
                "estrangelo edessa",
                "franklin gothic medium",
                "georgia",
                "lucida console",
                "lucida sans unicode",
                "mangal",
                "microsoft sans serif",
                "palatino linotype",
                "sylfaen",
                "tahoma",
                "times new roman",
                "trebuchet ms",
                "verdana"
        };

        public List<Color> RandomColor = new List<Color>()
        {
            Color.Red,
            Color.Green,
            Color.Blue,
            Color.Black,
            Color.Purple,
            Color.Orange
        };


        public Level FontWarp { get; set; } = Level.Medium;

        public Level BackgroundNoise { get; set; } = Level.High;

        public Level LineNoise { get; set; } = Level.High;
    }
}
