using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;
using System;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class RecommendationListPage : Page
    {
        #region Property

        #endregion

        #region Lifecycle

        public RecommendationListPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
            newsListBox.ItemsSource = recommendationNewsList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                pageTitle.Show("热门推荐");
                LoadRecommendation();
            }
        }

        #endregion

        #region Recommendation

        GenericDataLoader<Recommendation> recommendationLoader = new GenericDataLoader<Recommendation>();
        ObservableCollection<News> recommendationNewsList = new ObservableCollection<News>();
        private void LoadRecommendation()
        {
            if (recommendationLoader.Busy)
            {
                return;
            }

            //busy
            progressbar.Visibility = Visibility.Visible;

            //load
            recommendationLoader.Load("getrecommends", string.Empty, true, Constants.RECOMMENDATION_MODULE, Constants.RECOMMENDATION_FILE_NAME,
                result =>
                {
                    recommendationNewsList.Clear();
                    foreach (var item in result.data)
                    {
                        recommendationNewsList.Add(item);
                    }

                    //not busy
                    progressbar.Visibility = Visibility.Collapsed;

                });
        }

        #endregion

        private void news_ItemClick(object sender, ItemClickEventArgs e)
        {
            News news = e.ClickedItem as News;
            if (news == null)
            {
                return;
            }

            NewsHandler.OnNewsTap(this, news);
        }

    }
}
