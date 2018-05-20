using System;
using System.IO;
using Xamarin.Forms;
using SkiaSharp;
using System.Linq;

namespace XamForms.DesktopSample.Core
{
    public static class PaletteExtractor
    {
        public static Color[] Extract(FileInfo file, int paletteLength)
        {
            using (var bmp = SKBitmap.Decode(file.FullName))
            using(var scaled = bmp.Resize(new SKImageInfo(paletteLength / 2, paletteLength / 2), SKBitmapResizeMethod.Box))
            {
                return scaled.Pixels
                             .Take(paletteLength)
                             .Select(x => Color.FromRgba(x.Red, x.Green, x.Blue, x.Alpha))
                             .ToArray();
            }
        }
    }
}
