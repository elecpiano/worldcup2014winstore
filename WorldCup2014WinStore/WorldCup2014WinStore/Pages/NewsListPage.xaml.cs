﻿using System.Collections.Generic;
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
    public sealed partial class NewsListPage : Page
    {
        #region Property

        #endregion

        #region Lifecycle

        public NewsListPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
            newsListBox.ItemsSource = newsList;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            pageTitle.Show("新闻");
            LoadNews();
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
            if (newsLoader.Busy)
            {
                return;
            }

            //busy
            progressbar.Visibility = Visibility.Visible;

            //load
            newsLoader.Load("getnewslist", "&page=" + newsPageIndex.ToString(), true, Constants.NEWS_MODULE, string.Format(Constants.NEWS_LIST_FILE_NAME_FORMAT, newsPageIndex),
                list =>
                {
                    newsPageCount = list.TotalPageCount;

                    //remove more button
                    TryRemoveMoreButton();

                    if (newsReloading)
                    {
                        scrollViewer.ChangeView(0, null, null);
                        newsList.Clear();
                        newsListDateHeaders.Clear();
                    }

                    foreach (var item in list.data)
                    {
                        //if (!newsListDateHeaders.Contains(item.Time.Date))
                        //{
                        //    newsListDateHeaders.Add(item.Time.Date);
                        //    News dateHeader = new News() { IsDateHeader = true, HeaderDate = item.Time.Date };
                        //    newsList.Add(dateHeader);
                        //}

                        newsList.Add(item);
                    }

                    if (newsPageIndex < newsPageCount)
                    {
                        //add more button
                        EnsureMoreButton();
                    }

                    //not busy
                    progressbar.Visibility = Visibility.Collapsed;
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

        private void news_ItemClick(object sender, ItemClickEventArgs e)
        {
            News news = e.ClickedItem as News;
            if (news == null)
            {
                return;
            }

            if (news.IsMoreButton)
            {
                LoadMoreNews();
                return;
            }

            NewsHandler.OnNewsTap(this, news);
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
