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
        public static bool NavigatingFromHome = false;

        public NewsListPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (NavigatingFromHome)
            {
                PageMask.AttachAndOpen(this.maskPanel, () =>
                    {
                        PageTitle.AttachAndShow(this.pageTitlePanel, "News List");
                    });
            }
            else
            {
                PageTitle.AttachAndShow(this.pageTitlePanel, "News List");
            }
            PageTitle.OnBack = () =>
                {
                    PageMask.Close(() =>
                        {
                            PageTitle.TryGoBack();
                        });
                };
            NavigatingFromHome = false;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            PageMask.Detach();
            PageTitle.Detach();
            base.OnNavigatingFrom(e);
        }

        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PageTitle.Hide(() =>
            {
                this.Frame.Navigate(typeof(NewsDetailPage));
            });
        }

    }
}
