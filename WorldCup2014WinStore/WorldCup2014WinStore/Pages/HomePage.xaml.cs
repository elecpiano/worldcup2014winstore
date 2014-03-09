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
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            HomePage.AttachPageMaskAndOpen(this.maskPanel, "HomePage");
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            DetachPageMask();
            base.OnNavigatingFrom(e);
        }

        private void SwitchMask_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            HomePage.PageMask.Close(() =>
            {
                this.Frame.Navigate(typeof(NewsDetailPage));
            });

            //var properties = e.GetCurrentPoint(this).Properties;
            //if (properties.IsLeftButtonPressed)
            //{
            //}
            //else if (properties.IsRightButtonPressed)
            //{
            //}
        }

        #region Page Mask

        public static PageMask PageMask = new PageMask();
        public static void DetachPageMask()
        {
            if (PageMask.Parent != null)
            {
                ((Grid)PageMask.Parent).Children.Remove(PageMask);
            }
        }

        public static void AttachPageMaskAndOpen(Grid maskPanel, string pageTitle)
        {
            maskPanel.Children.Add(PageMask);
            PageMask.Open(pageTitle);
        }

        #endregion
    }
}
