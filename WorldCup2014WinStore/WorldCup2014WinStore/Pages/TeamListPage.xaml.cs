using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class TeamListPage : Page
    {
        private string subjectID = string.Empty;

        #region Lifecycle

        public TeamListPage()
        {
            InitializeComponent();
            this.TopAppBar = new NavBar(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pageTitle.Show("球队巡礼");
            LoadData();
        }

        #endregion

        #region data

        DataLoader<Subject> dataLoader = new DataLoader<Subject>();

        private void LoadData()
        {
            if (dataLoader.Loaded || dataLoader.Busy)
            {
                return;
            }

            //busy
            progressbar.Visibility = Visibility.Visible;

            //load
            dataLoader.Load("getteamlist", string.Empty, true, Constants.TEAM_MODULE, Constants.TEAM_FILE_NAME,
                result =>
                {
                    scrollViewer.ChangeView(0,null,null);
                    listBox.ItemsSource = result.NewsGroups;
                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        #endregion

        private void news_ItemClick(object sender, ItemClickEventArgs e)
        {
            News news = e.ClickedItem as News;
            if (news != null)
            {
                NewsHandler.OnNewsTap(this, news);
            }
        }

    }
}
