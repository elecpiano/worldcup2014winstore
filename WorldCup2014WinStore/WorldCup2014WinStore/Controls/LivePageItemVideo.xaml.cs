using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Pages;
using WorldCup2014WinStore.Utility;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class LivePageItemVideo : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemVideo()
        {
            this.InitializeComponent();
        }

        private void Control_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (HostingPage != null)
            {
                LiveLineItem item = sender.GetDataContext<LiveLineItem>();
                if (item != null)
                {
                    VideoPage.PlayVideo(HostingPage, item.ID, item.Title);
                }
            }
        }
    }
}
