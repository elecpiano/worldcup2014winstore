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
    public sealed partial class NewsListPage : Page
    {
        public NewsListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            PageMask.AttachAndOpen(this.maskPanel, "News Detail Page #001");
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            PageMask.DetachPageMask();
            base.OnNavigatingFrom(e);
        }

        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageMask.HidePageTitle(() =>
            {
                this.Frame.Navigate(typeof(NewsDetailPage));
            });
        }
    }
}
