using System;
using System.IO;
using XamForms.DesktopSample.Core;
using Gtk;

namespace XamForms.DesktopSample.GtkSharp
{
    public class ImageFileSelector : IImageFileSelector
    {
        public FileInfo PickImageFile()
        {
            var filter = new FileFilter { Name = "Image files" };
            filter.AddPattern("*.png");
            filter.AddPattern("*.jpg");
            var fcd = new FileChooserDialog("Choose image file", null, FileChooserAction.Open, "Cancel", ResponseType.Close, "Select", ResponseType.Accept)
            {
                SelectMultiple = false,
                Filter = filter
            };
            try
            {
                return fcd.Run() == (int)ResponseType.Accept ? new FileInfo(fcd.Filename) : null;
            }
            finally
            {
                fcd.Destroy();
            }
        }
    }
}
