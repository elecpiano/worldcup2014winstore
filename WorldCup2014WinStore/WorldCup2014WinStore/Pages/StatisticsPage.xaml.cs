using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;
using System.Collections.Generic;
using System;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class StatisticsPage : Page
    {
        #region Property

        App App { get { return App.Current as App; } }

        #endregion

        #region Lifecycle

        public StatisticsPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pageTitle.Show("数据世界杯");
            LoadHTML();
        }

        #endregion

        #region Data

        DataLoader<StatisticsAddress> dataLoader = new DataLoader<StatisticsAddress>();

        private void LoadHTML()
        {
            if (dataLoader.Busy)
            {
                return;
            }

            progressbar.Visibility = Visibility.Visible;

            dataLoader.Load("getconfig", string.Empty, false, string.Empty, string.Empty,
                result =>
                {
                    Uri uri = new Uri(result.URL);
                    browser.Navigate(uri);
                    progressbar.Visibility = Visibility.Collapsed;
                });
        }

        #endregion

    }
}
