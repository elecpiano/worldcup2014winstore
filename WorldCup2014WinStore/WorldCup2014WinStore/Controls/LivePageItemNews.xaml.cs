using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class LivePageItemNews : UserControl
    {
        public Page HostingPage { get; set; }

        public LivePageItemNews()
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
                    param.Add(NaviParam.NEWS_ID, item.ID);
                    param.Add(NaviParam.NEWS_TITLE, item.Title);

                    //TO-DO
                    //HostingPage.Frame.Navigate(typeof(LivePage), param);//NewsDetailPage
                }
            }
        }
    }
}
