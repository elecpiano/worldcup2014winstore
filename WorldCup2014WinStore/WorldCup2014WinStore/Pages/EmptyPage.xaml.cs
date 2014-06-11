﻿using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;
using Windows.UI.Xaml;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class EmptyPage : Page
    {
        #region Property

        #endregion

        #region Lifecycle

        public EmptyPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
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

            pageTitle.Show("xxxx");
        }

        #endregion

         
        //progressbar.Visibility = Visibility.Visible;
        //progressbar.Visibility = Visibility.Collapsed;

    }
}
