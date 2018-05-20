using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace XamForms.DesktopSample.Core
{
    public partial class App : Application
    {
        public IImageFileSelector ImageFileSelector { get; }

        public App(IImageFileSelector selector)
        {
            InitializeComponent();

            MainPage = new MainPage();
            ImageFileSelector = selector;
        }
    }
}
