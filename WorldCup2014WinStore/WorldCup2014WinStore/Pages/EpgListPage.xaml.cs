using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using System.Linq;
using System.Collections.ObjectModel;
using WorldCup2014WinStore.Utility;
using System;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class EpgListPage : Page
    {
        #region Property

        #endregion

        #region Lifecycle

        public EpgListPage()
        {
            InitializeComponent();
            this.TopAppBar = new NavBar(this);
            epgListBox.ItemsSource = epgList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                pageTitle.Show("TV");
                LoadEpg(DateTime.Today);
            }
        }

        #endregion

        #region EPG

        ObservableCollection<EPG> epgList = new ObservableCollection<EPG>();
        ListDataLoader<EPG> epgLoader = new ListDataLoader<EPG>();

        public void LoadEpg(DateTime date)
        {
            string dateStr = date.ToString("yyyy-MM-dd");
            LoadEpg(dateStr);
        }

        public void LoadEpg(string date)
        {
            if (epgLoader.Busy)
            {
                return;
            }

            this.gameDateTextBlock.Text = date;
            string param = "&date=" + date;

            progressbar.Visibility = Visibility.Visible;

            epgLoader.Load("getepg", param, true, Constants.EPG_MODULE, string.Format(Constants.EPG_FILE_NAME_FORMTAT, date),
                result =>
                {
                    if (result != null)
                    {
                        epgList.Clear();

                        foreach (var item in result)
                        {
                            epgList.Add(item);
                        }

                        UpdateEpgSubscriptionStatus(result);

                        epgListBox.ScrollIntoView(result.FirstOrDefault());

                        progressbar.Visibility = Visibility.Collapsed;
                    }
                });
        }

        private async void UpdateEpgSubscriptionStatus(List<EPG> list)
        {
            var subscriptionList = await SubscriptionDataContext.Current.LoadSubscriptions();
            if (subscriptionList == null)
            {
                return;
            }
            foreach (var item in list)
            {
                if (subscriptionList.Any(x => x.ID == item.ID))
                {
                    item.Subscribed = true;
                }
            }
        }

        private void epgListBox_ItemClick(object sender, ItemClickEventArgs e)
        {
            EPG epg = e.ClickedItem as EPG;
            if (epg == null)
            {
                return;
            }
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(NaviParam.LIVE_ID, epg.ID);
            param.Add(NaviParam.LIVE_IMAGE, epg.Image);
            param.Add(NaviParam.LIVE_TITLE, epg.Description);
            this.Frame.Navigate(typeof(LivePage), param);
        }

        #endregion

        private void calendar_DayTapped(object sender, TappedRoutedEventArgs e)
        {
            CalendarItem item = sender.GetDataContext<CalendarItem>();
            LoadEpg(item.Date);
        }

    }
}
