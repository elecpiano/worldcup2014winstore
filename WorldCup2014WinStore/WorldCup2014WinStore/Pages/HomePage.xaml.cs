using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Utility.Animations;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using System.Linq;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class HomePage : Page
    {
        #region Lifecycle

        public HomePage()
        {
            this.InitializeComponent();
            InitPage();
        }

        private void InitPage()
        {
            //BuildApplicationBar();
            //InitBannerControl();
            //InitEpgList();
            newsListBox.ItemsSource = newsList;
            //recommendationNewsListBox.ItemsSource = recommendationNewsList;
            //authorListBox.ItemsSource = authorList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //App.Toast = this.toast;

            LoadSplashImage();
            //LoadBanner();
            //if (ReloatEpgList)
            //{
            //    LoadEpg(true);
            //    ReloatEpgList = false;
            //}
            //LoadRecommendation();
            //LoadAuthorList();
            LoadNews();
        }

        #endregion

        #region Splash

        DataLoader<Splash> splashLoader = new DataLoader<Splash>();
        string openedSplashImageSource = string.Empty;
        private void LoadSplashImage()
        {
            //DisplayLocalSplashImage();

            if (splashLoader.Loaded || splashLoader.Busy)
            {
                return;
            }

            progressbar.Visibility = Visibility.Visible;

            splashLoader.Load("getsplash", string.Empty, true, Constants.SPLASH_MODULE, Constants.SPLASH_FILE_NAME,
                splash =>
                {
                    if (openedSplashImageSource == splash.Image)
                    {
                        return;
                    }
                    openedSplashImageSource = splash.Image;
                    MoveAnimation.MoveTo(splashImageBand, 0, 0, Constants.DURATION_CONTENT_FADING, fe =>
                        {
                            //this.splashImage.DataContext = splash.Image;
                            this.splashImage.Source = new BitmapImage(new Uri(splash.Image, UriKind.RelativeOrAbsolute));
                            progressbar.Visibility = Visibility.Collapsed;
                        });
                });
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

            string param = "&date=" + date;

            epgLoader.Load("getepg", param, true, Constants.EPG_MODULE, string.Format(Constants.EPG_FILE_NAME_FORMTAT, date),
                list =>
                {
                    if (list != null)
                    {
                        epgList.Clear();

                        foreach (var item in list)
                        {
                            epgList.Add(item);
                        }

                        var subscriptionList = GetSubscriptionList();
                        foreach (var item in list)
                        {
                            if (subscriptionList.Any(x => x.ID == item.ID))
                            {
                                item.Subscribed = true;
                            }
                        }

                        epgListBox.ScrollIntoView(list.FirstOrDefault());
                    }
                });
        }

        private void epgListBox_ItemClick(object sender, ItemClickEventArgs e)
        {
            //EPG epg = sender.GetDataContext<EPG>();
            //string[] paramArray = new string[] { NaviParam.LIVE_ID, epg.ID, NaviParam.LIVE_IMAGE, epg.Image, NaviParam.LIVE_TITLE, epg.Description };
            //string naviStr = string.Format("/Pages/LivePage.xaml?{0}={1}&{2}={3}&{4}={5}", paramArray);
            //HostingPage.NavigationService.Navigate(new Uri(naviStr, UriKind.Relative));
            //this.Frame.Navigate(typeof(AlbumListPage));
        }

        #endregion

        #region Subscription
        private List<EPG> GetSubscriptionList()
        {
            return SubscriptionDataContext.Current.LoadSubscriptions();
        }

        private void Subscribe_Tap(object sender, GestureEventArgs e)
        {
            
        }

        #endregion


        #region News

        GenericDataLoader<NewsList> newsLoader = new GenericDataLoader<NewsList>();
        ObservableCollection<News> newsList = new ObservableCollection<News>();
        int newsPageIndex = 1;
        int newsPageCount = 1;
        bool newsReloading = false;
        News newsMoreButtonItem = new News() { IsMoreButton = true };
        List<DateTime> newsListDateHeaders = new List<DateTime>();

        private void LoadNews()
        {
            if (newsLoader.Loaded || newsLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            newsLoader.Load("getnewslist", "&page=" + newsPageIndex.ToString(), true, Constants.NEWS_MODULE, string.Format(Constants.NEWS_LIST_FILE_NAME_FORMAT, newsPageIndex),
                list =>
                {
                    newsPageCount = list.TotalPageCount;

                    //remove more button
                    TryRemoveMoreButton();

                    if (newsReloading)
                    {
                        newsListScrollViewer.ChangeView(null, 0d, null);//.ScrollToVerticalOffset(0);
                        newsList.Clear();
                        newsListDateHeaders.Clear();
                    }

                    foreach (var item in list.data)
                    {
                        if (!newsListDateHeaders.Contains(item.Time.Date))
                        {
                            newsListDateHeaders.Add(item.Time.Date);
                            News dateHeader = new News() { IsDateHeader = true, HeaderDate = item.Time.Date };
                            newsList.Add(dateHeader);
                        }

                        newsList.Add(item);
                    }

                    if (newsPageIndex < newsPageCount)
                    {
                        //add more button
                        EnsureMoreButton();
                    }

                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        private void LoadMoreNews()
        {
            newsPageIndex++;
            if (newsPageIndex <= newsPageCount)
            {
                newsLoader.Loaded = false;
                newsReloading = false;
                LoadNews();
            }
        }

        private void ReloadNews()
        {
            //reset values
            newsPageIndex = 1;
            newsPageCount = 1;
            newsLoader.Loaded = false;
            newsReloading = true;
            LoadNews();
        }

        private void NewsItem_Click(object sender, RoutedEventArgs e)
        {
            News news = sender.GetDataContext<News>();
            if (news.IsMoreButton)
            {
                LoadMoreNews();
                return;
            }

            //TO-DO
            //NewsHandler.OnNewsTap(this, news);
        }

        private void TryRemoveMoreButton()
        {
            if (newsList.Contains(newsMoreButtonItem))
            {
                newsList.Remove(newsMoreButtonItem);
            }
        }

        private void EnsureMoreButton()
        {
            if (!newsList.Contains(newsMoreButtonItem))
            {
                newsList.Add(newsMoreButtonItem);
            }
        }

        #endregion


    }
}
