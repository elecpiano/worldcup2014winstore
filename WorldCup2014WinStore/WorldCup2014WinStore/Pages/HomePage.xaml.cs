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
using System.Threading.Tasks;
using WorldCup2014WinStore.Controls;

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
            epgListBox.ItemsSource = epgList;
            newsListBox.ItemsSource = newsList;
            recommendationNewsListBox.ItemsSource = recommendationNewsList;
            //authorListBox.ItemsSource = authorList;

            this.TopAppBar = new NavBar(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //App.Toast = this.toast;

            LoadSplashImage();
            //LoadBanner();
            LoadEpg(DateTime.Today.AddDays(-1));
            LoadRecommendation();
            LoadAuthorList();
            LoadNews_HomePage();
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
                    }
                });
        }

        private async void UpdateEpgSubscriptionStatus(List<EPG> list)
        {
            var subscriptionList = await GetSubscriptionList();
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
            if (epg==null)
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

        #region Subscription
        private async Task<List<EPG>> GetSubscriptionList()
        {
            return await SubscriptionDataContext.Current.LoadSubscriptions();
        }

        #endregion

        #region Recommendation

        GenericDataLoader<Recommendation> recommendationLoader = new GenericDataLoader<Recommendation>();
        ObservableCollection<News> recommendationNewsList = new ObservableCollection<News>();
        private const int RECOMMENDATION_COUNT = 6;
        private void LoadRecommendation()
        {
            if (recommendationLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            recommendationLoader.Load("getrecommends", string.Empty, true, Constants.RECOMMENDATION_MODULE, Constants.RECOMMENDATION_FILE_NAME,
                result =>
                {
                    //recommendationSlideShow.SetItemsSource(result.focus);

                    recommendationNewsList.Clear();
                    int count = 0;
                    foreach (var item in result.data)
                    {
                        recommendationNewsList.Add(item);
                        count++;
                        if (count >= RECOMMENDATION_COUNT)
                        {
                            break;
                        }
                    }

                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        #endregion

        #region News

        GenericDataLoader<NewsList> newsLoader = new GenericDataLoader<NewsList>();
        ObservableCollection<News> newsList = new ObservableCollection<News>();
        int newsPageIndex = 1;
        int newsPageCount = 1;
        bool newsReloading = false;
        News newsMoreButtonItem = new News() { IsMoreButton = true };
        //List<DateTime> newsListDateHeaders = new List<DateTime>();
        private const int NEWS_COUNT = 6;

        private void LoadNews_HomePage()
        {
            if (newsLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            newsLoader.Load("getnewslist", "&page=" + newsPageIndex.ToString(), true, Constants.NEWS_MODULE, string.Format(Constants.NEWS_LIST_FILE_NAME_FORMAT, newsPageIndex),
                list =>
                {
                    newsList.Clear();

                    int count = 0;
                    foreach (var item in list.data)
                    {
                        newsList.Add(item);
                        count++;
                        if (count >= NEWS_COUNT)
                        {
                            break;
                        }
                    }

                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        private void LoadNews()
        {
            if (newsLoader.Busy)
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
                        //newsListScrollViewer.ChangeView(null, 0d, null);//.ScrollToVerticalOffset(0);
                        newsList.Clear();
                        //newsListDateHeaders.Clear();
                    }

                    int count = 0;
                    foreach (var item in list.data)
                    {
                        //if (!newsListDateHeaders.Contains(item.Time.Date))
                        //{
                        //    newsListDateHeaders.Add(item.Time.Date);
                        //    News dateHeader = new News() { IsDateHeader = true, HeaderDate = item.Time.Date };
                        //    newsList.Add(dateHeader);
                        //}

                        newsList.Add(item);
                        count++;
                        if (count >= NEWS_COUNT)
                        {
                            break;
                        }
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

        #region Author

        GenericDataLoader<AuthorList> authorListLoader = new GenericDataLoader<AuthorList>();
        ObservableCollection<Author> authorList = new ObservableCollection<Author>();

        private void LoadAuthorList()
        {
            if (authorListLoader.Busy)
            {
                return;
            }

            //busy
            //snowNews.IsBusy = true;

            //load
            authorListLoader.Load("getauthorlist", string.Empty, true, Constants.AUTHOR_MODULE, Constants.AUTHOR_FILE_NAME,
                result =>
                {
                    authorList.Clear();

                    foreach (var item in result.data)
                    {
                        authorList.Add(item);
                    }

                    PopulateAuthorList();

                    //authorListBox.ScrollIntoView(authorList.FirstOrDefault());
                    authorListScrollViewer.ChangeView(null, 0d, null);

                    //not busy
                    //snowNews.IsBusy = false;
                });
        }

        private void PopulateAuthorList()
        {
            authorListPanel.Children.Clear();
            foreach (var item in authorList)
            {
                AuthorItem control = new AuthorItem();
                control.DataContext = item;
                if (item.Images != null && item.Images.Length > 0 && item.Images[0].Image != null)
                {
                    control.SetImageListVisibility(true);
                }
                authorListPanel.Children.Add(control);
            }
        }

        private void author_ItemClick(object sender, ItemClickEventArgs e)
        {
            Author author = sender.GetDataContext<Author>();
            string naviString = string.Format("/Pages/DiaryListPage.xaml?{0}={1}&{2}={3}&{4}={5}",
                NaviParam.AUTHOR_ID, author.ID,
                NaviParam.AUTHOR_NAME, author.Name,
                NaviParam.AUTHOR_FACE, author.Face);
            //NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
        }

        #endregion

    }
}
