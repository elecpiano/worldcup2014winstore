using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using WorldCup2014WinStore.Controls;
using WorldCup2014WinStore.Models;
using WorldCup2014WinStore.Utility;

namespace WorldCup2014WinStore.Pages
{
    public sealed partial class VideoPage : Page
    {
        private static List<VideoItem> videos = new List<VideoItem>();
        private string videoID = string.Empty;
        private string videoName = string.Empty;

        #region Lifecycle

        public VideoPage()
        {
            this.InitializeComponent();
            this.TopAppBar = new NavBar(this);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Dictionary<string, string> param = e.Parameter as Dictionary<string, string>;
            if (param != null)
            {
                videoID = param[NaviParam.VIDEO_ID];
                videoName = param[NaviParam.VIDEO_NAME];
            }

            pageTitle.Show(videoName);
            PlayVideo(videoID);
        }

        #endregion

        #region Data

        public static void PlayVideo(Page page, string id, string title )
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add(NaviParam.VIDEO_ID, id);
            param.Add(NaviParam.VIDEO_NAME, title);
            page.Frame.Navigate(typeof(VideoPage), param);
        }

        static DataLoader<VOD> dataLoader = new DataLoader<VOD>();

        private void PlayVideo(string vodID)
        {
            if (dataLoader.Busy)
            {
                return;
            }

            progressbar.Visibility = Visibility.Visible;

            dataLoader.Load("getvod", "&id=" + vodID, false, Constants.VOD_MODULE, string.Format(Constants.VOD_FILE_NAME_FORMAT, vodID),
                data =>
                {
                    progressbar.Visibility = Visibility.Collapsed;
                    if (data != null && data.URL != null && !string.IsNullOrEmpty(data.URL.Trim()))
                    {
                        PlaySingleVideo(data.URL);
                    }

                    //if (data.Videos != null && data.Videos.Length > 0)
                    //{
                    //    videos.Clear();
                    //    foreach (var item in data.Videos)
                    //    {
                    //        videos.Add(item);
                    //    }

                    //    if (hostingPage != null)
                    //    {
                    //        string naviString = string.Format("/Pages/VideoPage.xaml");
                    //        hostingPage.NavigationService.Navigate(new Uri(naviString, UriKind.Relative));
                    //    }
                    //}
                    //else
                    //{
                    //    PlaySingleVideo(data.URL);
                    //}

                });
        }

        #endregion

        //private void Video_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        //{
        //    VideoItem item = sender.GetDataContext<VideoItem>();

        //    MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
        //    mediaPlayerLauncher.Media = new Uri(item.URL, UriKind.Absolute);
        //    mediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop;
        //    mediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;
        //    mediaPlayerLauncher.Show();
        //}

        private void PlaySingleVideo(string url)
        {
            mediaElement.Source = new Uri(url, UriKind.Absolute);
            mediaElement.Play();

            //MediaPlayerLauncher mediaPlayerLauncher = new MediaPlayerLauncher();
            //mediaPlayerLauncher.Media = new Uri(url, UriKind.Absolute);
            //mediaPlayerLauncher.Controls = MediaPlaybackControls.Pause | MediaPlaybackControls.Stop;
            //mediaPlayerLauncher.Orientation = MediaPlayerOrientation.Landscape;
            //mediaPlayerLauncher.Show();
        }
    }
}
