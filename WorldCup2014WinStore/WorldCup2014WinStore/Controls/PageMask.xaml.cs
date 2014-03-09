using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class PageMask : UserControl
    {
        private Action onClosed;

        private bool isOpen = false;
        public bool IsOpen
        {
            get
            {
                return isOpen;
            }
            set
            {
                if (isOpen != value)
                {
                    isOpen = value;
                    double maskOpacity = isOpen ? 0d : 1d;
                    this.restOfMask.Opacity = maskOpacity;
                    this.spinningBall.Opacity = maskOpacity;
                    this.ring0.Opacity = maskOpacity;
                    this.ring1.Opacity = maskOpacity;
                    this.ring2.Opacity = maskOpacity;
                    this.ring3.Opacity = maskOpacity;
                    this.ring4.Opacity = maskOpacity;
                    this.ring5.Opacity = maskOpacity;
                    this.ring6.Opacity = maskOpacity;
                    this.fillup.Opacity = maskOpacity;
                }
            }
        }

        public PageMask()
        {
            this.InitializeComponent();
        }

        public void Open(string pageTitle)
        {
            this.StoryOpen.Begin();
            this.StoryShowPageTitle.Begin();
            this.pageTitleTextBlock.Text = pageTitle;
        }

        public void Close(Action completed = null)
        {
            onClosed = completed;
            this.StoryClose.Begin();
        }

        private void StoryOpen_Completed(object sender, object e)
        {
        }

        private void StoryClose_Completed(object sender, object e)
        {
            if (onClosed != null)
            {
                onClosed();
            }
        }

    }
}
