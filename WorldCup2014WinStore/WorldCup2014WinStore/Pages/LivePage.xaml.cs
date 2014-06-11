using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class LivePage : Page
    {
        #region Property

        private string liveID = string.Empty;
        private string liveTitle = string.Empty;
        private string liveImage = string.Empty;

        #endregion

        #region Lifecycle

        public LivePage()
        {
            InitializeComponent();
            this.TopAppBar = new NavBar(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                Dictionary<string, string> param = e.Parameter as Dictionary<string, string>;
                if (param != null)
                {
                    liveID = param[NaviParam.LIVE_ID];
                    liveImage = param[NaviParam.LIVE_IMAGE];
                    liveTitle = param[NaviParam.LIVE_TITLE];
                }

                //this.titleImage.Source = new BitmapImage(new Uri(liveImage, UriKind.RelativeOrAbsolute));
                pageTitle.Show(liveTitle);

                LoadData();
            }
            StartTimer();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            StopTimer();
        }

        #endregion

        #region Data

        GenericDataLoader<LiveData> liveLoader = new GenericDataLoader<LiveData>();

        private void LoadData()
        {
            if (liveLoader.Busy)
            {
                return;
            }

            progressbar.Visibility = Visibility.Visible;

            liveLoader.Load("getlivepage", "&id=" + liveID, true, Constants.LIVE_MODULE, string.Format(Constants.LIVE_FILE_NAME_FORMAT, liveID),
                data =>
                {
                    PopulateScore(data);
                    PopulateRankList(data);
                    PopulateLineItems(data);

                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        private void PopulateScore(LiveData data)
        {
            if (data.Score != null)
            {
                scorePanel.DataContext = data.Score;
                scorePanel.Visibility = Visibility.Visible;
            }
            else
            {
                scorePanel.Visibility = Visibility.Collapsed;
            }
        }

        private void PopulateRankList(LiveData data)
        {
            if (data.RankList != null)
            {
                rankListBox.ItemsSource = data.RankList;
                rankListBox.Visibility = Visibility.Visible;
            }
            else
            {
                rankListBox.ItemsSource = null;
                rankListBox.Visibility = Visibility.Collapsed;
            }
        }

        private void PopulateLineItems(LiveData data)
        {
            lineItemsStackPanel.Children.Clear();

            if (data == null)
            {
                return;
            }
            if (data.LineItems == null)
            {
                return;
            }

            FrameworkElement control = null;

            foreach (var item in data.LineItems)
            {
                switch (item.Type.ToString())
                {
                    case "0"://video
                        control = new LivePageItemVideo() { HostingPage = this };
                        break;
                    case "1"://news
                        control = new LivePageItemNews() { HostingPage = this };
                        break;
                    case "2"://album
                        if (item.Album.Length > 3)
                        {
                            List<AlbumItem> newList = new List<AlbumItem>();
                            for (int i = 0; i < 3; i++)
                            {
                                newList.Add(item.Album[i]);
                            }
                            item.Album = newList.ToArray();
                        }
                        control = new LivePageItemAlbum() { HostingPage = this };
                        break;
                    case "12"://live text
                        control = new LivePageItemLiveText() { HostingPage = this };
                        break;
                    default:
                        control = null;
                        break;
                }

                if (control != null)
                {
                    control.DataContext = item;
                    lineItemsStackPanel.Children.Add(control);
                }
            }
        }

        #endregion

        #region Timer

        private DispatcherTimer timer = null;
        private void StartTimer()
        {
            if (timer == null)
            {
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(30);
                timer.Tick += timer_Tick;
            }
            timer.Start();
        }

        void timer_Tick(object sender, object e)
        {
            liveLoader.Loaded = false;
            LoadData();
        }

        private void StopTimer()
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }

        #endregion

    }
}
