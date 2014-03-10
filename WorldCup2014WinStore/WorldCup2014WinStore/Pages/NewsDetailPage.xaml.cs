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
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class NewsDetailPage : Page
    {
        public NewsDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PageMask.AttachAndShowPageTitle(this.maskPanel, "News Detail Page #001");
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            PageMask.DetachPageMask();
            base.OnNavigatingFrom(e);
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            return;
            Frame frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                if (frame.CanGoBack)
                {
                    PageMask.Close(() =>
                    {
                        frame.GoBack();
                    });
                }
            }
        }
    }
}
