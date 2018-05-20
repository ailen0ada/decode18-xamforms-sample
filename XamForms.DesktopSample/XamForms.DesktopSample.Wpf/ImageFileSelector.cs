using System;
using System.IO;
using Microsoft.Win32;
using XamForms.DesktopSample.Core;

namespace XamForms.DesktopSample.Wpf
{
    public class ImageFileSelector : IImageFileSelector
    {
        public FileInfo PickImageFile()
        {
            var ofd = new OpenFileDialog
            {
                Filter = "画像ファイル(*.png,*.jpg)|*.png;*.jpg"
            };
            var result = ofd.ShowDialog();
            return result == true ? new FileInfo(ofd.FileName) : null;
        }
    }
}
