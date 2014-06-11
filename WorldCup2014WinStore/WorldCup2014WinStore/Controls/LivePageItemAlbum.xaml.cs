using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Pages;
using WorldCup2014WinStore.Utility;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class LivePageItemAlbum : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemAlbum()
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
                    Dictionary<string, string> param = new Dictionary<string, string>();
                    param.Add(NaviParam.ALBUM_ID, item.ID);
                    HostingPage.Frame.Navigate(typeof(AlbumPage), param);
                }
            }
        }
    }
}
