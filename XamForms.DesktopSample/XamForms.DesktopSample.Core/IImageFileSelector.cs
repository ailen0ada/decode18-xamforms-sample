using System;
using System.IO;
namespace XamForms.DesktopSample.Core
{
    public interface IImageFileSelector
    {
        FileInfo PickImageFile();
    }
}
