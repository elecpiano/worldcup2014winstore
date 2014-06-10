using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Pages;

namespace WorldCup2014WinStore.Controls
{
    public class NewsHandler
    {
        public static void OnNewsTap(Page hostingPage, News news)
        {
            string naviString = string.Empty;
            Dictionary<string, string> param = null;

            switch (news.Type)
            {
                case "0":
                    VideoPage.PlayVideo(hostingPage, news.ID, news.Title);
                    break;
                case "1":
                    param = new Dictionary<string, string>();
                    param.Add(NaviParam.NEWS_ID, news.ID);
                    param.Add(NaviParam.NEWS_TITLE, news.Title);
                    param.Add(NaviParam.NEWS_IMAGE, news.Image);
                    hostingPage.Frame.Navigate(typeof(NewsDetailPage), param);
                    break;
                case "2":
                    naviString = string.Format("/Pages/AlbumPage.xaml?{0}={1}", NaviParam.ALBUM_ID, news.ID);
                    //hostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                    break;
                case "31":
                    param = new Dictionary<string, string>();
                    param.Add(NaviParam.SUBJECT_ID, news.ID);
                    hostingPage.Frame.Navigate(typeof(SubjectPage), param);
                    break;
                case "15":
                    param = new Dictionary<string, string>();
                    param.Add(NaviParam.SUBJECT_ID, news.ID);
                    hostingPage.Frame.Navigate(typeof(SubjectPage), param);
                    break;
                default:
                    break;
            }
        }

        public static void OnNewsTap(Page hostingPage, string id, string title, string image, string secondaryHeader = "")
        {
            string naviString = string.Format("/Pages/NewsDetailPage.xaml?{0}={1}&{2}={3}&{4}={5}", new object[] { NaviParam.NEWS_ID, id, NaviParam.NEWS_TITLE, title, NaviParam.NEWS_IMAGE, image });
            if (!string.IsNullOrEmpty(secondaryHeader))
            {
                naviString = string.Format(naviString + "&{0}={1}", NaviParam.NEWS_SECOND_TITLE, secondaryHeader);
            }
            //hostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
        }
    }
}
