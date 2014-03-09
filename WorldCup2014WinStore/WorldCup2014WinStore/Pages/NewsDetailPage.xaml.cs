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

namespace WorldCup2014WinStore.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsDetailPage : Page
    {
        public NewsDetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            HomePage.AttachPageMaskAndOpen(this.maskPanel, "News Detail Page #001");
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            HomePage.DetachPageMask();
            base.OnNavigatingFrom(e);
        }

        private void Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Frame frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                if (frame.CanGoBack)
                {
                    HomePage.PageMask.Close(() =>
                    {
                        frame.GoBack();
                    });
                }
            }
        }
    }
}
