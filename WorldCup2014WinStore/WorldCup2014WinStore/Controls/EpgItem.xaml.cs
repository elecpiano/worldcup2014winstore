using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;

namespace WorldCup2014WinStore.Controls
{
    public sealed partial class EpgItem : UserControl
    {
        public EpgItem()
        {
            this.InitializeComponent();
        }

        private void Subscribe_Click(object sender, RoutedEventArgs e)
        {
            EPG epg = sender.GetDataContext<EPG>();
            if (epg.Subscribed)
            {
                ReminderHelper.RemoveReminder(epg.ID);
                SubscriptionDataContext.Current.RemoveSubscription(epg.ID);
                epg.Subscribed = false;
                App.ShowToastMessage("成功取消预约。");
            }
            else
            {
                try
                {
                    var successful = ReminderHelper.AddReminder(epg.ID, epg.Category, epg.Match, epg.StartTime, "/Pages/HomePage.xaml?");
                    if (successful)
                    {
                        SubscriptionDataContext.Current.AddSubscription(epg);
                        epg.Subscribed = true;
                        App.ShowToastMessage("成功添加预约。");
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
