using System;
using System.IO;
using XamForms.DesktopSample.Core;
using AppKit;
namespace XamForms.DesktopSample.Mac
{
    public class ImageFileSelector : IImageFileSelector
    {
        public FileInfo PickImageFile()
        {
            var ofd = new NSOpenPanel
            {
                AllowedFileTypes = new[] { "png", "jpg" }
            };
            var ret = ofd.RunModal();
            return ret < 1 ? null : new FileInfo(ofd.Url.Path);
        }
    }
}
