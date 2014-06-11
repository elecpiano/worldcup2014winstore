using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class StadiumListPage : Page
    {
        #region Property

        #endregion

        #region Lifecycle

        public StadiumListPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
            stadiumListBox.ItemsSource = stadiumList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //Dictionary<string, string> param = e.Parameter as Dictionary<string, string>;
            //if (param != null)
            //{
            //    newsID = param[NaviParam.NEWS_ID];
            //    newsTitle = param[NaviParam.NEWS_TITLE];
            //    newsImage = param[NaviParam.NEWS_IMAGE];
            //}

            pageTitle.Show("球场巡礼");
            LoadStadiums();
        }

        #endregion

        #region StadiumList

        ObservableCollection<Stadium> stadiumList = new ObservableCollection<Stadium>();
        ListDataLoader<Stadium> stadiumLoader = new ListDataLoader<Stadium>();

        private void LoadStadiums()
        {
            if (stadiumLoader.Loaded || stadiumLoader.Busy)
            {
                return;
            }

            progressbar.Visibility = Visibility.Visible;

            stadiumLoader.Load("getstadiumlist", string.Empty, true, Constants.STADIUM_MODULE, Constants.STADIUM_LIST_FILE_NAME,
                list =>
                {
                    stadiumList.Clear();
                    foreach (var item in list)
                    {
                        stadiumList.Add(item);
                    }
                    stadiumListBox.ScrollIntoView(null);
                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        private void list_ItemClick(object sender, ItemClickEventArgs e)
        {
            Stadium stadium = e.ClickedItem as Stadium;
            if (stadium == null)
            {
                return;
            }
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(NaviParam.STADIUM_ID, stadium.ID);
            param.Add(NaviParam.STADIUM_NAME, stadium.NameCN);
            this.Frame.Navigate(typeof(StadiumDetailPage), param);
        }

        #endregion


    }
}
