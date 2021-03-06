﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamForms.DesktopSample.Core
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void SelectImageFile(object sender, System.EventArgs e)
        {
            var selector = (App.Current as Core.App)?.ImageFileSelector;
            if (selector == null) return;

            var file = selector.PickImageFile();
            if (file == null || !file.Exists) return;

            this.PathLabel.Text = file.FullName;
            this.ImageView.Source = ImageSource.FromFile(file.FullName);

            var colors = PaletteExtractor.Extract(file, 4);
            this.ColorTip1.Color = colors[0];
            this.ColorTip2.Color = colors[1];
            this.ColorTip3.Color = colors[2];
            this.ColorTip4.Color = colors[3];
        }
    }
}
